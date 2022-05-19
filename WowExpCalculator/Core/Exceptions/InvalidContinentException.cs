using System;
using WowExpCalculator.Core.Enums;

namespace WowExpCalculator.Core.Exceptions;

public class InvalidContinentException : Exception
{
    public InvalidContinentException(Continents continent) : base($"Continent {nameof(continent)} not supported")
    {
    }
}