using System.Text.Json.Serialization;

namespace BnFurniture.Domain.Responses;

public class ApiBaseResponse
{
    [JsonInclude]
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess;

    [JsonInclude]
    [JsonPropertyName("statusCode")]
    public int StatusCode;

    [JsonInclude]
    [JsonPropertyName("message")]
    public string? Message;

    [JsonInclude]
    [JsonPropertyName("errors")]
    public Dictionary<string, List<string>>? Errors;


    public ApiBaseResponse(
        bool isSuccess,
        int statusCode,
        string? message = null,
        Dictionary<string, List<string>>? errors = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Errors = errors;
    }
}
