using FluentAssertions;

using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests.Users;

public sealed class UserTests
{
    [Fact]
    public void Create_Should_CreateUser()
    {
        var userId = UserId.NewUserId();
        var name = UserName.From("Meng Syue Tsai").Value;
        var email = Email.From("mengsyue.tsai@outlook.com").Value;
        var passwordHash = "password-hash";
        var now = DateTimeOffset.UtcNow;

        ErrorOr<User> errorOrUser = User.Create(
            id: userId,
            name: name,
            email: email,
            emailConfirmed: false,
            passwordHash: passwordHash,
            lockoutEnabled: false,
            accessFailedCount: 0,
            createdAt: now);

        errorOrUser.IsValue.Should().BeTrue();

        var user = errorOrUser.Value;
        user.Id.Should().Be(userId);
        user.Name.Should().Be(name);
        user.Email.Should().Be(email);
        user.EmailConfirmed.Should().BeFalse();
        user.PasswordHash.Should().Be(passwordHash);
        user.LockoutEnabled.Should().BeFalse();
        user.AccessFailedCount.Should().Be(0);
        user.CreatedAt.Should().Be(now);
    }

    [Fact]
    public void AddAccount_When_AccountAlreadyExists_Should_ReturnFailureResult()
    {
        var userId = UserId.NewUserId();
        var name = UserName.From("Meng Syue Tsai").Value;
        var email = Email.From("mengsyue.tsai@outlook.com").Value;
        var passwordHash = "password-hash";
        var now = DateTimeOffset.UtcNow;

        User user = User
            .Create(
                id: userId,
                name: name,
                email: email,
                emailConfirmed: false,
                passwordHash: passwordHash,
                lockoutEnabled: false,
                accessFailedCount: 0,
                createdAt: now)
            .ThrowIfError()
            .Value;

        var account1 = Account
            .Create(
                AccountId.From("account-id").ThrowIfFailure().Value,
                user.Id,
                "account-name")
            .ThrowIfError()
            .Value;

        var account2 = Account
            .Create(
                AccountId.From("account-id").ThrowIfFailure().Value,
                user.Id,
                "account-name")
            .ThrowIfError()
            .Value;

        var result1 = user.AddAccount(account1);
        var result2 = user.AddAccount(account2);

        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeFalse();
        user.Accounts.Should().HaveCount(1);
    }

    [Fact]
    public void AddAccount_Should_ReturnSuccessResult()
    {
        var userId = UserId.NewUserId();
        var name = UserName.From("Meng Syue Tsai").Value;
        var email = Email.From("mengsyue.tsai@outlook.com").Value;
        var passwordHash = "password-hash";
        var now = DateTimeOffset.UtcNow;

        User user = User
            .Create(
                id: userId,
                name: name,
                email: email,
                emailConfirmed: false,
                passwordHash: passwordHash,
                lockoutEnabled: false,
                accessFailedCount: 0,
                createdAt: now)
            .ThrowIfError()
            .Value;

        var account = Account
            .Create(
                AccountId.From("account-id").ThrowIfFailure().Value,
                user.Id,
                "account-name")
            .ThrowIfError()
            .Value;

        var result = user.AddAccount(account);

        result.IsSuccess.Should().BeTrue();
        user.Accounts.Should().Contain(account);
        user.Accounts.Count.Should().Be(1);
    }
}