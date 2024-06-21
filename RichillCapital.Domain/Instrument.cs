using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol symbol,
        string description,
        string exchange)
        : base(symbol)
    {
        Description = description;
        Exchange = exchange;
    }

    public Symbol Symbol => Id;

    public string Description { get; private set; }

    public string Exchange { get; private set; }

    public static ErrorOr<Instrument> Create(
        Symbol symbol,
        string description,
        string exchange)
    {
        var instrument = new Instrument(
            symbol,
            description,
            exchange);

        return ErrorOr<Instrument>.With(instrument);
    }
}
