using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Account : Entity<AccountId>
{
    private Account(
        AccountId id,
        UserId userId,
        string name,
        DateTimeOffset createdAt)
        : base(id)
    {
        UserId = userId;
        Name = name;
        CreatedAt = createdAt;
    }

    public UserId UserId { get; private set; }

    public string Name { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public static ErrorOr<Account> Create(
        AccountId id,
        UserId userId,
        string name,
        DateTimeOffset createdAt)
    {
        var account = new Account(
            id,
            userId,
            name,
            createdAt);

        return ErrorOr<Account>.With(account);
    }
}
