using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Symbol : SingleValueObject<string>
{
    public const int MaxLength = 64;

    private Symbol(string value)
        : base(value)
    {
    }

    public static Result<Symbol> From(string value) => value
        .ToResult()
        .Ensure(
            symbol => !string.IsNullOrWhiteSpace(symbol),
            Error.Invalid("Symbol cannot be empty."))
        .Ensure(
            symbol => symbol.Length <= MaxLength,
            Error.Invalid($"Symbol cannot be longer than {MaxLength} characters."))
        .Then(symbol => new Symbol(symbol));
}