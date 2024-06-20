using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class UserId : SingleValueObject<string>
{
    private const string Prefix = "UID";
    public const int MaxLength = 10;

    private UserId(string value) :
        base(value)
    {
    }

    public static UserId NewUserId()
    {
        string randomDigits = new Random()
            .Next(0, (int)Math.Pow(10, MaxLength - 3))
            .ToString()
            .PadLeft(MaxLength - 3, '0');

        return From($"{Prefix}{randomDigits}").Value;
    }

    public static Result<UserId> From(string value) => value
        .ToResult()
        .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid("UserId cannot be empty"))
        .Ensure(id => id.StartsWith(Prefix), Error.Invalid("UserId must start with 'UID'"))
        .Ensure(id => id.Length == MaxLength, Error.Invalid($"UserId must be {MaxLength} characters long"))
        .Then(id => new UserId(id));
}