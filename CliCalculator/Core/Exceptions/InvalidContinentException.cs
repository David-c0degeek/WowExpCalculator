using CliCalculator.Core.Enums;

namespace CliCalculator.Core.Exceptions;

public class InvalidContinentException : Exception
{
    public InvalidContinentException(Continents continent) : base($"Continent {nameof(continent)} not supported")
    {
    }
}