export interface SliceRejectResponse {
  message: string | null;
  errors?: Record<string, string[]>;
}