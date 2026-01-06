namespace EcommerceLearn.Application.Emails;

public sealed record EmailVerificationModel(
    string VerificationLink,
    string FirstName,
    string LastName,
    string Code
);

public sealed record ResetPasswordModel(
    string ResetLink,
    string FirstName
);