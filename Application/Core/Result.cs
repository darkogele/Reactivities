using System.Collections.Generic;
using System.Net;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public HttpStatusCode? StatusCodes { get; set; }
        public Dictionary<string, string> ValidationError { get; set; }

        public static Result<T> Success(T data) => new()
        {
            IsSuccess = true, Value = data
        };

        public static Result<T> Failure(string error, HttpStatusCode? code = null) => new()
        {
            IsSuccess = false, Error = error, StatusCodes = code
        };

        public static Result<T> ValidationFailure(Dictionary<string, string> error) => new()
        {
            IsSuccess = false,
            ValidationError = error,
        };
    }
}