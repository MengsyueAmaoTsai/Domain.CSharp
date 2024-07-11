using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSource : Entity<SignalSourceId>
{
    private SignalSource(
        SignalSourceId id,
        string name,
        string description,
        DateTimeOffset createdAt)
        : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public static ErrorOr<SignalSource> Create(
        SignalSourceId id,
        string name,
        string description,
        DateTimeOffset createdAt)
    {
        var signalSource = new SignalSource(
            id: id,
            name: name,
            description: description,
            createdAt: createdAt);

        return ErrorOr<SignalSource>.With(signalSource);
    }
}
