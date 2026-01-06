namespace EcommerceLearn.Domain.Common.Entities;

public class Verification : Entity<int>
{
    public DateTime ExpiredAt { get; protected set; } = DateTime.UtcNow.AddMinutes(15);
    public string Code { get; protected set; } = string.Empty;
    public int Attempts { get; protected set; } = 0;

    public void IncrementAttempts()
    {
        Attempts++;
    }

    public void DeleteCode()
    {
        Code = string.Empty;
    }
}