using CliCalculator.Core.Enums;
using CliCalculator.Core.Exceptions;

namespace CliCalculator.Core;

public class TbcPlayerLevel : PlayerLevel
{
    private const double MaximumPlayerLevel = 70;

    protected override void Validate()
    {
        if (Value > MaximumPlayerLevel)
            throw new InvalidPlayerLevelException(Expansion.TheBurningCrusade, Value);
    }
}