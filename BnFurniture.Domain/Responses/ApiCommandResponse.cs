namespace BnFurniture.Domain.Responses;

public sealed class ApiCommandResponse : ApiBaseResponse
{
    public ApiCommandResponse(
        bool isSuccess,
        int statusCode, 
        string? message = null,
        Dictionary<string, List<string>>? errors = null) 
            : base(isSuccess, statusCode, message, errors)
    {
    }
}
