namespace Demo.API.Errors
{
    public class ApiExceptionServer : ApiErrorResponse
    {
        public ApiExceptionServer(int statusCode, string? message = null, string? details=null) : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }

    }
}
