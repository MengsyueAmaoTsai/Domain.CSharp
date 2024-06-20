using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class Email : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private Email(string value)
        : base(value)
    {
    }

    public static Result<Email> From(string value) => value
        .ToResult()
        .Ensure(
            email => !string.IsNullOrEmpty(email),
            Error.Invalid("Email cannot be empty"))
        .Ensure(
            email => email.Length <= MaxLength,
            Error.Invalid($"Email cannot be longer than {MaxLength} characters"))
        .Ensure(
            email => email.Contains('@'),
            Error.Invalid("Email must contain '@'"))
        .Ensure(
            email => email.Contains('.'),
            Error.Invalid("Email must contain '.'"))
        .Then(email => new Email(email));
}