namespace CleanArchitecture.WebApi.Models
{
    public record ErrorResponse(
        bool success,
        int statusCode,
        string message
    );
}