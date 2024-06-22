import { useState, useEffect, useRef, useCallback  } from "react";
import axios, { AxiosError } from "axios";
import { ApiQueryResponse } from "../types/api/ApiResponseTypes";

export const useFetchApiQueryResponse = <T,>(
  endpoint: string,
  params?: object
) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<AxiosError | null>(null);

  const controllerRef = useRef<AbortController | null>(null);

  const fetchData = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    const controller = new AbortController();
    controllerRef.current = controller;

    try {
      const { data } = await axios.get<ApiQueryResponse<T>>(endpoint, {
        params,
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
  }, [endpoint, params]);

  useEffect(() => {
    fetchData();

    return () => {
      controllerRef.current?.abort();
    };
  }, [fetchData]);

  return { response, isLoading, error };
};

export const useFetchApiPostQueryResponse = <T,>(
  endpoint: string,
  body: object
) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<AxiosError | null>(null);

  const controllerRef = useRef<AbortController | null>(null);

  // Stabilize the body dependency
  const bodyString = JSON.stringify(body);

  const postData = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    const controller = new AbortController();
    controllerRef.current = controller;

    try {
      const { data } = await axios.post<ApiQueryResponse<T>>(endpoint, JSON.parse(bodyString), {
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
  }, [endpoint, bodyString]); // Ensure stable body and endpoint dependencies

  useEffect(() => {
    postData();

    return () => {
      controllerRef.current?.abort(); // Cleanup by aborting the request
    };
  }, [postData]); // Only runs when postData changes

  return { response, isLoading, error };
};


/*
export const useFetchApiPostQueryResponse = <T,>(
  endpoint: string,
  body: object
) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<AxiosError | null>(null);

  const controllerRef = useRef<AbortController | null>(null);

  const postData = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    const controller = new AbortController();
    controllerRef.current = controller;

    try {
      const { data } = await axios.post<ApiQueryResponse<T>>(endpoint, body, {
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
  }, [endpoint, body]);

  useEffect(() => {
    postData();

    return () => {
      controllerRef.current?.abort();
    };
  }, [postData]);

  return { response, isLoading, error };
};
*/

/*
export const useFetchApiQueryResponse = <T,>(
  endpoint: string,
  params?: object
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
*/