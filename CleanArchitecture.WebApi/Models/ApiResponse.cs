namespace CleanArchitecture.WebApi.Models
{
    public record ApiResponse<T>(
    bool Success,
    int StatusCode,
    string Message,
    T? Data
)
    {
        public static ApiResponse<T> SuccessResponse(
            int statusCode,
            string message,
            T? data = default)
        {
            return new ApiResponse<T>(
                true,
                statusCode,
                message,
                data
            );
        }
    }
}