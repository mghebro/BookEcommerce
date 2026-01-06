namespace EcommerceLearn.Domain.Common.Results;

public sealed record Errors(int Status, string Message)
{
    // 400 - Bad Request
    public static Errors Invalid(string msg) => new(400, msg);
    public static Errors ValidationFailed() => new(400, "Validation failed");
    
    // 401 - Unauthorized
    public static Errors Unauthorized() => new(401, "Unauthorized access");
    public static Errors InvalidCredentials() => new(401, "Invalid credentials");
    
    // 403 - Forbidden
    public static Errors Forbidden() => new(403, "Access forbidden");
    public static Errors InsufficientPermissions() => new(403, "Insufficient permissions");
    
    // 404 - Not Found
    public static Errors NotFound(string msg) => new(404, msg);
    public static Errors NotFound() => new(404, "Resource not found");
    
    // 409 - Conflict
    public static Errors Conflict() => new(409, "Conflict occurred");
    public static Errors AlreadyExists() => new(409, "Resource already exists");

    public static class Categories
    {
        public static Errors NotFound(int id) => new(404, $"Category with id '{id}' not found.");
    }
}