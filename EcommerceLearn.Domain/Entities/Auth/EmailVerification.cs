using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Auth;

public class EmailVerification : Verification
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public static EmailVerification Create()
    {
        Random random = new();
        var code = random.Next(100_000, 9999_99).ToString();

        return new EmailVerification { Code = code };
    }

    public void SetUserId(int userId)
    {
        UserId = userId;
    }
}