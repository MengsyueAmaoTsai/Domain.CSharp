using FluentAssertions;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests;

public sealed class InstrumentTests
{
    [Fact]
    public void Create_Should_CreateInstrument()
    {
        var symbol = Symbol.From("AAPL").ThrowIfFailure().Value;
        var description = "Apple Inc.";
        var exchange = "NASDAQ";

        ErrorOr<Instrument> errorOrInstrument = Instrument.Create(
            symbol,
            description,
            exchange);

        errorOrInstrument.IsValue.Should().BeTrue();

        var instrument = errorOrInstrument.Value;
        instrument.Id.Should().Be(symbol);
        instrument.Description.Should().Be(description);
        instrument.Exchange.Should().Be(exchange);
    }
}