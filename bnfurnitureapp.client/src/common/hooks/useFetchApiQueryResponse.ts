import { useState, useEffect, useRef } from 'react';
import axios from "axios";
import { ApiQueryResponse } from "../types/ApiResponseTypes";

export const useFetchApiQueryResponse = <T,>(endpoint: string, params: object) => {
  const [response, setResponse] = useState<ApiQueryResponse<T> | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  const initialParamsRef = useRef(params);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const { data } = await axios.get<ApiQueryResponse<T>>(endpoint, { params: initialParamsRef.current });
        setResponse(data);
      } catch (error) {
        setError(error as Error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [endpoint]);

  return { response, isLoading, error };
};