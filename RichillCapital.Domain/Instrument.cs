using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(Symbol id)
        : base(id)
    {
    }

    public static ErrorOr<Instrument> Create(Symbol symbol)
    {
        var instrument = new Instrument(symbol);

        return ErrorOr<Instrument>.With(instrument);
    }
}
