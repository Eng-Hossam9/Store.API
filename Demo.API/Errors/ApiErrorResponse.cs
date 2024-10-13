namespace Demo.API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message??GetMesasgeForStatusCode(statusCode);
        }
        

        public int StatusCode { get; set; }
        public string? Message { get; set; }



        public string GetMesasgeForStatusCode(int statusCode) 
        {
            var massage = statusCode switch
            {
                400=> "Bad Request Error occurred",
                401=>"Your Are Not Authorized",
                404=> "Resources Not Found ",
                500=>"Server Error",
                _=>null
            };

            return massage;
        }
    }
}
