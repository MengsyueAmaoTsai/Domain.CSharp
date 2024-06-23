using FluentAssertions;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests;

public sealed class SignalSourceTests
{
    [Fact]
    public void Create_Should_CreateSignalSource()
    {
        var signalSourceId = SignalSourceId
            .From("TV-Long-Task")
            .ThrowIfFailure()
            .ValueOrDefault;

        var name = "TV-Long-Task";
        var description = "TV-Long-Task description";
        var createdAt = DateTimeOffset.UtcNow;

        ErrorOr<SignalSource> errorOrSignalSource = SignalSource.Create(
            id: signalSourceId,
            name: name,
            description: description,
            createdAt: createdAt);

        errorOrSignalSource.IsValue.Should().BeTrue();
        var signalSource = errorOrSignalSource.Value;

        signalSource.Id.Should().Be(signalSourceId);
        signalSource.Name.Should().Be(name);
        signalSource.Description.Should().Be(description);
        signalSource.CreatedAt.Should().Be(createdAt);
    }
}