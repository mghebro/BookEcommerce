using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Auth;

public class PasswordVerification : Verification
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public static PasswordVerification Create()
    {
        Random random = new();
        var code = random.Next(100_000, 9999_99).ToString();

        return new PasswordVerification { Code = code };
    }

    public void SetUserId(int userId)
    {
        UserId = userId;
    }
}