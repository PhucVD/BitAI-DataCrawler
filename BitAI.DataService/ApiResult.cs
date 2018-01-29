using Newtonsoft.Json;
using System;

namespace BitAI.DataService
{
    public class ApiResult<T>
    {
        public ApiResult(bool success, String message, T result)
        {
            Success = success;
            Message = message;
            Result = result;
        }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
}
