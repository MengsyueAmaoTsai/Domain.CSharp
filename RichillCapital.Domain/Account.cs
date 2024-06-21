using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Account : Entity<AccountId>
{
    private Account(
        AccountId id,
        UserId userId,
        string name)
        : base(id)
    {
        UserId = userId;
        Name = name;
    }

    public UserId UserId { get; private set; }

    public string Name { get; private set; }

    public static ErrorOr<Account> Create(
        AccountId id,
        UserId userId,
        string name)
    {
        var account = new Account(
            id,
            userId,
            name);

        return ErrorOr<Account>.With(account);
    }
}
