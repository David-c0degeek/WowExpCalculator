using CliCalculator.Core.Enums;

namespace CliCalculator.Core.Exceptions;

public class InvalidPlayerLevelException : Exception
{
    public InvalidPlayerLevelException(Expansion expansion, ushort value): base($"Player level cannot be {value} in {nameof(expansion)}")
    {
        
    }
}