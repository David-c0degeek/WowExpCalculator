using System;
using WowExpCalculator.Core.Enums;
using WowExpCalculator.Core.Exceptions;

namespace WowExpCalculator.Core;

public static class ExpCalculator
{
    public static uint CalculateExp(TbcPlayerLevel playerLevel, uint mobLevel, Continents continent,
        TbcPlayerLevel highestGroupMemberLevel, byte groupSize = 1, bool isElite = false, bool isRested = false)
    {
        if (IsMobGray(playerLevel, mobLevel)) return 0;

        if (!Constants.ContinentMobExpBonus.TryGetValue(continent, out var continentExpModifier))
            throw new InvalidContinentException(continent);

        var baseExpGained = mobLevel * 5 + continentExpModifier;
        
        var soloExp = mobLevel > playerLevel.Value
            ? baseExpGained * (1 + 0.05 * (mobLevel - playerLevel.Value))
            : baseExpGained * (1 - (playerLevel.Value - mobLevel) / GetZeroDifference(playerLevel));

        var resultExp = groupSize switch
        {
            1 => soloExp,
            2 => Math.Round(soloExp * playerLevel.Value / (highestGroupMemberLevel.Value + playerLevel.Value)),
            _ => Math.Round(soloExp) / groupSize * GetGroupModifier(groupSize)
        };
        
        return (uint)(resultExp + GetExpBonuses(resultExp, isElite, isRested));
    }

    private static uint GetExpBonuses(double soloExp, bool isElite, bool isRested)
    {
        var eliteModifier = isElite ? 0.5 : 0;
        var restedModifier = isRested ? 0.5 : 0;

        return (uint)(soloExp * eliteModifier + soloExp * restedModifier);
    }

    private static ushort GetZeroDifference(TbcPlayerLevel playerLevel)
    {
        return playerLevel.Value switch
        {
            >= 1 and <= 7 => 5,
            >= 8 and <= 9 => 6,
            >= 10 and <= 11 => 7,
            >= 12 and <= 15 => 8,
            >= 16 and <= 19 => 9,
            >= 20 and <= 29 => 11,
            >= 30 and <= 39 => 12,
            >= 40 and <= 44 => 13,
            >= 45 and <= 49 => 14,
            >= 50 and <= 54 => 15,
            >= 55 and <= 59 => 16,
            >= 60 and <= 84 => 17,
            _ => throw new ArgumentOutOfRangeException(nameof(playerLevel))
        };
    }

    private static bool IsMobGray(TbcPlayerLevel charLevel, uint mobLevel)
    {
        var playerLevel = charLevel.Value;

        if (playerLevel <= 5) return false;

        var grayLevel = playerLevel switch
        {
            >= 5 and <= 39 => playerLevel - (ushort)Math.Floor((double)playerLevel / 10) - 5,
            >= 40 and <= 59 => playerLevel - (ushort)Math.Floor((double)playerLevel / 5) - 1,
            >= 60 and <= 70 => playerLevel - 9,
            _ => throw new ArgumentOutOfRangeException(nameof(charLevel))
        };

        return mobLevel <= grayLevel;
    }

    public static MobColor GetMobColor(TbcPlayerLevel playerLevel, uint mobLevel)
    {
        var pLvl = playerLevel.Value;

        if (mobLevel >= pLvl + 5) return MobColor.Red;
        if (mobLevel == pLvl + 3 || mobLevel == pLvl + 4) return MobColor.Orange;
        if (mobLevel <= pLvl + 2 && mobLevel >= pLvl - 2) return MobColor.Yellow;
        if (mobLevel <= pLvl - 3 && !IsMobGray(playerLevel, mobLevel)) return MobColor.Green;

        return MobColor.Gray;
    }

    private static double GetGroupModifier(byte groupSize)
    {
        var groupExpModifier = 1.0;

        switch (groupSize)
        {
            case 1:
            case 2:
                groupExpModifier = 1;
                break;
            case 3:
                groupExpModifier = 1.166;
                break;
            case 4:
                groupExpModifier = 1.3;
                break;
            case 5:
                groupExpModifier = 1.4;
                break;
        }

        return groupExpModifier;
    }
}