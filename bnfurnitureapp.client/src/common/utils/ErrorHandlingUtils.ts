import axios, { AxiosError } from "axios";
import { ApiErrorResponse } from "../types/api/ApiResponseTypes";
import { SliceRejectResponse } from "../types/SliceRejectResponse";
import log from "loglevel";

export const handleApiError = (error: unknown, actionName: string) => {
  const errorMessage = getErrorMessage(error);
  let errorResponse: SliceRejectResponse = { message: errorMessage };
  logError(actionName, errorMessage, error);

  if (error instanceof AxiosError && error.response?.data) {
    const responseData = error.response.data as ApiErrorResponse;
    errorResponse = {
      message: `${responseData.statusCode}: ${responseData.message}`,
      errors: responseData.errors,
    };
  }
  return errorResponse;
};

export const getErrorMessage = (error: unknown): string => {
  if (axios.isAxiosError(error)) {
    if (
      error.response &&
      error.response.data &&
      typeof error.response.data.message === "string"
    ) {
      return error.response.data.message;
    }
    return error.message;
  }

  if (error instanceof Error) {
    return error.message;
  }

  if (error && typeof error === "object" && "message" in error) {
    return String(error.message);
  }

  if (typeof error === "string") {
    return error;
  }

  return "Unknown Error";
};

export const logError = (prefix: string, message: string, error: unknown) => {
  log.error(`'${prefix}' Server Error: ${message}`, error);
};

/*
export const getErrorMessage = (error: unknown): string => {
  let message: string;

  if (error instanceof Error) {
    message = error.message;
  } else if (axios.isAxiosError(error)) {
    message = error.message;
  } else if (error && typeof error === "object" && "message" in error) {
    message = String(error.message);
  } else if (typeof error === "string") {
    message = error;
  } else {
    message = "Unknown Error";
  }
  
  return message;
}
*/
