namespace Demo.API.Errors
{
    public class ApiValidationErrorResponse:ApiErrorResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }= new List<string>();
    }
}
