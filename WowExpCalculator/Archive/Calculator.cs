using System;

namespace WowExpCalculator.Archive;

public class Calculator
{
    
    // https://wowpedia.fandom.com/wiki/Experience_to_level
    // https://wowwiki-archive.fandom.com/wiki/Formulas:Mob_XP
    // https://wowpedia.fandom.com/wiki/Mob_experience
    
    
    // Mob XP Functions (including Con Colors)
// Colors will be numbers:
//  {grey = 0, green = 1, yellow = 2, orange = 3, red = 4, skull = 5}
// NOTE: skull = red when working with anything OTHER than mobs!

    private static double GetConColor(uint playerLevel, uint mobLevel)
    {
        if (playerLevel + 5 <= mobLevel)
        {
            return playerLevel + 10 <= mobLevel ? 5 : 4;
        }

        switch ((int)mobLevel - playerLevel)
        {
            case 4:
            case 3:
                return 3;
            case 2:
            case 1:
            case 0:
            case -1:
            case -2:
                return 2;
            default:
                return playerLevel switch
                {
                    // More adv formula for grey/green lvls:
                    <= 5 => 1, //All others are green.
                    <= 39 when mobLevel <=
                               playerLevel - 5 -
                               Math.Floor((double)playerLevel / 10) => 0, // Its below or equal to the 'grey level':
                    <= 39 => 1,
                    _ => mobLevel <= (playerLevel - 1 - Math.Floor((double)playerLevel / 5)) ? 0 : 1
                };
        }
    }

    private static int GetZd(uint lvl)
    {
        return lvl switch
        {
            <= 7 => 5,
            <= 9 => 6,
            <= 11 => 7,
            <= 15 => 8,
            <= 19 => 9,
            <= 29 => 11,
            <= 39 => 12,
            <= 44 => 13,
            <= 49 => 14,
            <= 54 => 15,
            <= 59 => 16,
            _ => 17
        };
    }

    private static double GetMobXp(uint playerLevel, uint mobLevel)
    {
        if (mobLevel >= playerLevel)
        {
            var temp = ((playerLevel * 5) + 45) * (1 + (0.05 * (mobLevel - playerLevel)));
            var tempCap = ((playerLevel * 5) + 45) * 1.2;
            return Math.Floor(temp > tempCap ? tempCap : temp);
        }

        if (GetConColor(playerLevel, mobLevel) == 0)
        {
            return 0;
        }

        return Math.Floor(((double)playerLevel * 5) + 45) * (1 - (playerLevel - mobLevel) / GetZd(playerLevel));
    }

    private static double GetEliteMobXp(uint playerLevel, uint mobLevel)
    {
        return GetMobXp(playerLevel, mobLevel) * 2;
    }

// Rested Bonus:
// Restedness is double XP, but if we only have part restedness we must split the XP:
    private static double GetMobXpFull(uint playerLevel, uint mobLevel, bool elite, int rest)
    {
        // rest = how much XP is left before restedness wears off:

        var temp = elite ? GetEliteMobXp(playerLevel, mobLevel) : GetMobXp(playerLevel, mobLevel);

        // Now to apply restedness.  temp = actual XP.
        // If restedness is 0...
        if (rest == 0)
        {
            return temp;
        }

        if (rest >= temp)
        {
            return temp * 2;
        }

        //Restedness is partially covering the XP gained.
        // XP = rest + (AXP - (rest / 2))
        return rest + (temp - (rest / 2));
    }

// Party Mob XP:
private double GetPartyMobXpFull(uint playerLevel, uint highestLevel, uint sumLevels, uint mobLevel, bool elite, int rest)
    {
        var temp = GetMobXpFull(highestLevel, mobLevel, elite, 0);
        // temp = XP from soloing via highest lvl...
        temp = temp * playerLevel / sumLevels;
        if (rest == 0)
        {
            return temp;
        }

        if (rest >= temp)
        {
            return temp * 2;
        }

        //Restedness is partially covering the XP gained.
        // XP = rest + (AXP - (rest / 2))
        return rest + (temp - (rest / 2));
    }
}