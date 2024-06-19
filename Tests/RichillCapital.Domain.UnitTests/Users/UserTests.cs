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
        var now = DateTimeOffset.UtcNow;

        ErrorOr<User> errorOrUser = User.Create(
            userId,
            name,
            email,
            emailConfirmed: false,
            createdAt: now);

        errorOrUser.IsValue.Should().BeTrue();

        var user = errorOrUser.Value;
        user.Id.Should().Be(userId);
        user.Name.Should().Be(name);
        user.Email.Should().Be(email);
        user.EmailConfirmed.Should().BeFalse();
        user.CreatedAt.Should().Be(now);
    }
}