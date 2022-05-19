using WowExpCalculator.Core.Enums;
using WowExpCalculator.Core.Exceptions;

namespace WowExpCalculator.Core;

public class TbcPlayerLevel : PlayerLevel
{
    private const double MaximumPlayerLevel = 70;

    protected override void Validate()
    {
        if (Value > MaximumPlayerLevel)
            throw new InvalidPlayerLevelException(Expansion.TheBurningCrusade, Value);
    }
}