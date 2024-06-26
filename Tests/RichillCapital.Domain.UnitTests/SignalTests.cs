using FluentAssertions;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.UnitTests;

public sealed class SignalTests
{
    [Fact]
    public void Create_Should_CreateSignal()
    {
        var signalId = SignalId.NewSignalId();
        var sourceId = "TV-Long-Task";
        var now = DateTime.UtcNow;
        var exchange = "exchange";
        var symbol = "symbol";
        var quantity = 1;
        var price = 1;
        var ipAddress = "1.1.1.1";
        var latency = 1535;

        ErrorOr<Signal> errorOrSignal = Signal.Create(
            id: signalId,
            sourceId: sourceId,
            time: now,
            exchange: exchange,
            symbol: symbol,
            quantity: quantity,
            price: price,
            ipAddress: ipAddress,
            latency: latency);

        errorOrSignal.IsValue.Should().BeTrue();
        var signal = errorOrSignal.Value;
        signal.Id.Should().Be(signalId);
        signal.SourceId.Should().Be(sourceId);
        signal.Time.Should().Be(now);
        signal.Exchange.Should().Be(exchange);
        signal.Symbol.Should().Be(symbol);
        signal.Quantity.Should().Be(quantity);
        signal.Price.Should().Be(price);
        signal.IpAddress.Should().Be(ipAddress);
        signal.Latency.Should().Be(latency);
    }
}