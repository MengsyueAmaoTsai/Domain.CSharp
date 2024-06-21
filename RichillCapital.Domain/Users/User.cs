using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<Account> _accounts = [];

    private User(
        UserId id,
        UserName name,
        Email email,
        bool emailConfirmed,
        string passwordHash,
        bool lockoutEnabled,
        int accessFailedCount,
        DateTimeOffset createdAt)
        : base(id)
    {
        Name = name;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        LockoutEnabled = lockoutEnabled;
        AccessFailedCount = accessFailedCount;
        CreatedAt = createdAt;
    }

    public UserName Name { get; private set; }

    public Email Email { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public string PasswordHash { get; private set; }

    public bool LockoutEnabled { get; private set; } = true;

    public int AccessFailedCount { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public IReadOnlyList<Account> Accounts => _accounts.ToList();

    public static ErrorOr<User> Create(
        UserId id,
        UserName name,
        Email email,
        bool emailConfirmed,
        string passwordHash,
        bool lockoutEnabled,
        int accessFailedCount,
        DateTimeOffset createdAt)
    {
        var user = new User(
            id: id,
            name: name,
            email: email,
            emailConfirmed: emailConfirmed,
            passwordHash: passwordHash,
            lockoutEnabled: lockoutEnabled,
            accessFailedCount: accessFailedCount,
            createdAt: createdAt);

        return ErrorOr<User>.With(user);
    }

    public Result AddAccount(Account account)
    {
        if (_accounts.Any(acc => acc == account))
        {
            var error = Error
                .Conflict("Users.AccountAlreadyExists", $"Account with id {account.Id} already exists.");

            return Result.Failure(error);
        }

        _accounts.Add(account);

        return Result.Success;
    }
}
