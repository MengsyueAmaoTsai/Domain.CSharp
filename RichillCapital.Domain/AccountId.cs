using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class AccountId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    private AccountId(string value)
        : base(value)
    {
    }

    public static Result<AccountId> From(string value) => value
        .ToResult()
        .Then(id => new AccountId(id));
}