interface ApiBaseResponse {
  isSuccess: boolean;
  statusCode: number;
  message?: string;
  errors?: Record<string, string[]>;
}

interface ApiCommandResponse extends ApiBaseResponse {

}

interface ApiQueryResponse<T> extends ApiBaseResponse {
  data?: T;
}

export type { ApiCommandResponse, ApiQueryResponse };