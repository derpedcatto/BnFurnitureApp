namespace BnFurniture.Domain.Responses;

public class StatusResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public StatusResponse(
        bool isSuccess,
        int statusCode,
        string message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
}
