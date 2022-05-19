using System;
using WowExpCalculator.Core.Enums;

namespace WowExpCalculator.Core.Exceptions;

public class InvalidPlayerLevelException : Exception
{
    public InvalidPlayerLevelException(Expansion expansion, ushort value): base($"Player level cannot be {value} in {nameof(expansion)}")
    {
        
    }
}