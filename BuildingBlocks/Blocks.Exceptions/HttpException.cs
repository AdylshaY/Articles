namespace Blocks.Exceptions
{
    using System.Net;

    public class HttpException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public HttpException(HttpStatusCode statusCode, string message) : base(string.IsNullOrEmpty(message) ? statusCode.ToString() : message)
        {
            this.HttpStatusCode = statusCode;
        }

        public HttpException(HttpStatusCode statusCode, string message, Exception ex) : base(message, ex)
        {
            this.HttpStatusCode = statusCode;
        }
    }
}
