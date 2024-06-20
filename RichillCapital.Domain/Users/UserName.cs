using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class UserName : SingleValueObject<string>
{
    public const int MaxLength = 50;

    private UserName(string value)
        : base(value)
    {
    }

    public static Result<UserName> From(string value) => value
        .ToResult()
        .Ensure(
            name => !string.IsNullOrEmpty(name),
            Error.Invalid("UserName cannot be empty"))
        .Ensure(
            name => name.Length <= MaxLength,
            Error.Invalid($"UserName cannot be longer than {MaxLength} characters"))
        .Then(name => new UserName(name));
}
