namespace BnFurniture.Domain.Responses;

public class StatusResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 0;

    public StatusResponse(
        bool isSuccess,
        int statusCode = 0,
        string message = "No message response")
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
}

public class StatusResponse<T> : StatusResponse
{
    public T? Data { get; set; }

    public StatusResponse(bool isSuccess, int statusCode = 0, string message = "", T? data = default)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }
}