namespace BnFurniture.Domain.Responses
{
    public class ApiCommandResponse
    {
        public ApiCommandResponse(bool success, int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Добавляем свойство Data
        public Dictionary<string, List<string>> Errors { get; set; } // Изменяем тип свойства Errors
    }
}
