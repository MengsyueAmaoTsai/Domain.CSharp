using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceId : SingleValueObject<string>
{
    private SignalSourceId(string value)
        : base(value)
    {
    }

    public static Result<SignalSourceId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new SignalSourceId(id));
}