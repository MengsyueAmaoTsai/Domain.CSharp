using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class User : Entity<UserId>
{
    private User(
        UserId id,
        UserName name,
        Email email,
        bool emailConfirmed,
        DateTimeOffset createdAt)
        : base(id)
    {
        Name = name;
        Email = email;
        EmailConfirmed = emailConfirmed;
        CreatedAt = createdAt;
    }

    public UserName Name { get; private set; }

    public Email Email { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public static ErrorOr<User> Create(
        UserId id,
        UserName name,
        Email email,
        bool emailConfirmed,
        DateTimeOffset createdAt)
    {
        var user = new User(
            id,
            name,
            email,
            emailConfirmed: emailConfirmed,
            createdAt: createdAt);

        return ErrorOr<User>.With(user);
    }
}
