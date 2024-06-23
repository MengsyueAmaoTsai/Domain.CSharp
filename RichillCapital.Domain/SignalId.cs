using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalId : SingleValueObject<Guid>
{
    private SignalId(Guid value)
        : base(value)
    {
    }

    public static Result<SignalId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new SignalId(Guid.Parse(id)));

    public static SignalId NewSignalId() => new(Guid.NewGuid());
}

