using System.Net;

namespace SettlementService.Models
{
    public class Result
    {
        public string ErrorMessage { get; set; }

        // error code
        public HttpStatusCode httpStatusCode { get; set; }

        public Result(string errorMessage, HttpStatusCode httpStatusCode)
        {
            ErrorMessage = errorMessage;
            this.httpStatusCode = httpStatusCode;
        }

        public Result(HttpStatusCode httpStatusCode)
        {
            this.httpStatusCode = httpStatusCode;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        public Result(string errorMessage, HttpStatusCode httpStatusCode) : base(errorMessage, httpStatusCode)
        {
        }

        public Result(T value, HttpStatusCode httpStatusCode) : base(null, httpStatusCode)
        {
            Value = value;
        }

        public Result(HttpStatusCode httpStatusCode) : base(httpStatusCode)
        {
        }
    }

}
