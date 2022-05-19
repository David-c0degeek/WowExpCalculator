using CliCalculator.Core.Enums;

namespace CliCalculator.Core;

public static class Constants
{
    public static Dictionary<Continents, ushort> ContinentMobExpBonus = new Dictionary<Continents, ushort>(3)
    {
        { Continents.Azeroth, 45 },
        { Continents.Outland, 235 },
        { Continents.Northrend, 580 },
    };
}