using FluentAssertions;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests;

public sealed class InstrumentTests
{
    [Fact]
    public void Create_Should_CreateInstrument()
    {
        var symbol = Symbol.From("AAPL").ThrowIfFailure().Value;

        ErrorOr<Instrument> errorOrInstrument = Instrument.Create(symbol);
        errorOrInstrument.IsValue.Should().BeTrue();

        var instrument = errorOrInstrument.Value;
        instrument.Id.Should().Be(symbol);
    }
}