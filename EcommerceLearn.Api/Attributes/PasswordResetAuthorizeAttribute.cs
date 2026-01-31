using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using EcommerceLearn.Infrastructure.Security;

namespace EcommerceLearn.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class PasswordResetAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var jwtSettings = context.HttpContext.RequestServices.GetRequiredService<JwtSettings>();

        var authHeader = context.HttpContext.Request.Headers["resetpassword"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Password reset token is required" });
            return;
        }

        var token = authHeader["Bearer ".Length..].Trim();

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            var pwdResetClaim = principal.FindFirst("pwd_reset")?.Value;

            if (pwdResetClaim != "true")
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Invalid password reset token" });
                return;
            }

            var email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                        ?? principal.FindFirst(ClaimTypes.Email)?.Value;

            context.HttpContext.Items["email"] = email;
            context.HttpContext.Items["code"] = principal.FindFirst("CODE")?.Value;
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Password reset token has expired" });
        }
        catch (Exception)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Invalid password reset token" });
        }
    }
}