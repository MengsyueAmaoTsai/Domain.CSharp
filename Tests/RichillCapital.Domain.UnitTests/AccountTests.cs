using FluentAssertions;

using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests;

public sealed class AccountTests
{
    [Fact]
    public void Create_Should_CreateAccount()
    {
        var expectedAccountId = AccountId.From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
        var expectedUserId = UserId.From("UID0000001").ThrowIfFailure().Value;
        var expectedName = "Test account";

        var errorOrAccount = Account.Create(
            expectedAccountId,
            expectedUserId,
            expectedName);

        errorOrAccount.IsValue.Should().BeTrue();

        var account = errorOrAccount.Value;
        account.Id.Should().Be(expectedAccountId);
        account.UserId.Should().Be(expectedUserId);
        account.Name.Should().Be(expectedName);
    }
}