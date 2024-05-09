using System.Text.Json.Serialization;

namespace BnFurniture.Domain.Responses;

public sealed class ApiQueryResponse<TResponse> : ApiBaseResponse
{
    [JsonInclude]
    [JsonPropertyName("data")]
    public TResponse? Data;

    public ApiQueryResponse(
        bool isSuccess, 
        int statusCode,
        string? message = null,
        TResponse? data = default,
        Dictionary<string, List<string>>? errors = null) 
            : base(isSuccess, statusCode, message, errors)
    {
        Data = data;
    }
}