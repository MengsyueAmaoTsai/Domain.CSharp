using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        string sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        decimal quantity,
        decimal price)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Exchange = exchange;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
    }

    public string SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    public string Exchange { get; private set; }

    public string Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal Price { get; private set; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        string sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        decimal quantity,
        decimal price)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            exchange,
            symbol,
            quantity,
            price);

        return ErrorOr<Signal>.With(signal);
    }
}

