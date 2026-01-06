using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceLearn.Api.Extensions.Auth;

public static class UserExtensions
{
    public static int GetUserId(this ControllerBase controller)
    {
        var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        if (!int.TryParse(userIdClaim, out int userId))
            throw new ValidationException("User ID claim is not a valid integer.");

        return userId;
    }
}