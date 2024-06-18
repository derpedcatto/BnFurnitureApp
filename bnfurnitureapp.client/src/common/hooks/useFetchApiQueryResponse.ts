import { useState, useEffect, useRef, useCallback  } from "react";
import axios, { AxiosError } from "axios";
import { ApiQueryResponse } from "../types/api/ApiResponseTypes";

export const useFetchApiQueryResponse = <T,>(
  endpoint: string,
  params: object
) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<AxiosError | null>(null);

  const initialParamsRef = useRef(params);
  const endpointRef = useRef(endpoint);
  const controllerRef = useRef<AbortController | null>(null);

  const fetchData = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    const controller = new AbortController();
    controllerRef.current = controller;

    try {
      const { data } = await axios.get<ApiQueryResponse<T>>(endpointRef.current, {
        params: initialParamsRef.current,
        signal: controller.signal,
      });
      setResponse(data);
    } catch (error) {
      if (!axios.isCancel(error)) {
        setError(error as AxiosError);
      }
    } finally {
      if (!controller.signal.aborted) {
        setIsLoading(false);
      }
    }
  }, [endpointRef, initialParamsRef]);

  useEffect(() => {
    fetchData();

    return () => {
      controllerRef.current?.abort();
    };
  }, [fetchData]);

  return { response, isLoading, error };
};

/*
export const useFetchApiQueryResponse = <T>(
  endpoint: string,
  params: object
) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  const initialParamsRef = useRef(params);
  const endpointRef = useRef(endpoint);

  useEffect(() => {
    let isCancelled = false;

    const fetchData = async () => {
      try {
        const { data } = await axios.get<ApiQueryResponse<T>>(
          endpointRef.current,
          { params: initialParamsRef.current }
        );
        if (!isCancelled) {
          setResponse(data);
        }
      } catch (error) {
        setError(error as Error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();

    return () => {
      isCancelled = true;
    };
  }, []);

  return { response, isLoading, error };
}
*/

/*
export const useFetchApiQueryResponse = <T,>(endpoint: string, params: object) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  const initialParamsRef = useRef(params);
  const endpointRef = useRef(endpoint);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const { data } = await axios.get<ApiQueryResponse<T>>(endpointRef.current, { params: initialParamsRef.current });
        setResponse(data);
      } catch (error) {
        setError(error as Error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, []);

  return { response, isLoading, error };
};
*/
