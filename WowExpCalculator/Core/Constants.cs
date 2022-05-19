using System.Collections.Generic;
using WowExpCalculator.Core.Enums;

namespace WowExpCalculator.Core;

public static class Constants
{
    public static Dictionary<Continents, ushort> ContinentMobExpBonus = new Dictionary<Continents, ushort>(3)
    {
        { Continents.Azeroth, 45 },
        { Continents.Outland, 235 },
        { Continents.Northrend, 580 },
    };
}