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
}