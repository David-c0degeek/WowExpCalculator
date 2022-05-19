# WowExpCalculator
Simple exp calculator for Azeroth, Northrend, Outlannds (Vanilla, Classic, Wotlk)

Values / formulas based on https://wowpedia.fandom.com/wiki/Mob_experience

------------------------------------------------------------------------------



Mob experience
View source
Contents

    1 Con Colors
    2 Basic Formula
    3 Higher Level Mobs
    4 Lower Level Mobs
    5 Elite Mobs
    6 Rested Bonus
    7 Solo Experience Modifiers
    8 Level 60 and Up Characters
    9 Group Experience
        9.1 Two Character Groups
    10 Example Code

Con Colors

This is the color used to display the mob's level number in when the mob is targeted. It is a function of the difference between your level and the mob's level. Click Here For a full table list per level
  
Red:     Mob Level >= Char Level + 5  
Orange:  Mob Level =  Char Level + (3 or 4)  
Yellow:  Mob Level =  Char Level ± 2  
Green:   Mob Level <= Char Level - 3, but ab  ove Gray Level  
Gray:    Mob Level <= Gray Level

The highest mob level that gives you no experience is the Gray Level. It varies based on your character level as follows: (notes where levels have not been confirmed)

Char level  1-5:  Gray level = 0 (all mobs give XP)  
Char level  6-39: Gray level = Char level - FLOOR(Char level/10) - 5  
Char level 40-59: Gray level = Char level - FLOOR(Char level/5) - 1  
Char level 60-70: Gray level = Char level - 9 = 51 @ 60, 61 @ 70  

Basic Formula

The basic formula has changed in Patch 4.03a to be relative to the Mob's level and not your own.

The amount of experience you will get for a solo kill on a mob whose level is equal to your level is:

XP = (Mob Level * 5) + 45, where Char Level = Mob Level, for mobs in Azeroth  
XP = (Mob Level * 5) + 235, where Char Level = Mob Level, for mobs in Outland  
XP = (Mob Level * 5) + 580, where Char Level = Mob Level, for mobs in Northrend   

For example, a level 60 character killing a level 60 mob in Azeroth gets (60 * 5) + 45 = 345 exp.   
Killing a level 60 mob in Outland gives (60 * 5) + 235 = 535 exp.

Higher Level Mobs

XP = (Base XP) * (1 + 0.05 * (Mob Level - Char Level) ), where Mob Level > Char Level

This is the amount of experience you will get for a solo kill on a mob whose level is higher than your level. This is known to be valid for up to Mob Level = Char Level + 4 (Orange and high-Yellow Mobs). Red mobs cap out at the same experience as orange. Higher elite mobs may also cap at the same amount (ie are not doubled).
Lower Level Mobs

For a given character level, the amount of XP given by lower-level mobs is a linear function of the Mob Level. The amount of experience reaches zero when the difference between the Char Level and Mob Level reaches a certain point. This is called the Zero Difference value, and is given by:

ZD =  5, when Char Level =  1 -  7  
ZD =  6, when Char Level =  8 -  9  
ZD =  7, when Char Level = 10 - 11  
ZD =  8, when Char Level = 12 - 15  
ZD =  9, when Char Level = 16 - 19   
ZD = 11, when Char Level = 20 - 29  
ZD = 12, when Char Level = 30 - 39  
ZD = 13, when Char Level = 40 - 44  
ZD = 14, when Char Level = 45 - 49  
ZD = 15, when Char Level = 50 - 54  
ZD = 16, when Char Level = 55 - 59  
ZD = 17, when Char Level = 60 - 84  

Using the ZD values above, the formula for Mob XP for any mob of level lower than your character is:

XP = (Base XP) * (1 - (Char Level - Mob Level)/ZD )
   where Mob Level < Char Level, and Mob Level > Gray Level

Example calculations:

using Char Level = 20.  
so Gray Level = 13, by the table above.  
killing any mob level 13 or lower will give 0 XP.  
Basic XP is (Mob level * 5 + 45).   
Killing a level 20 mob will give (20 * 5 + 45) = 145 XP.  
For a level 21 mob, we have XP = (21 * 5 + 45) * (1 + 0.05 * 1) = 157.5 rounded to 158 XP.  
ZD is 11, from the table above.   
For a level 18 mob, we have XP = (18 * 5 + 45) * (1 - 2/11) = 110.4 rounded to 110 XP.  
For a level 16 mob, we have XP = (16 * 5 + 45) * (1 - 4/11) =  79.54 rounded to  80 XP.  
For a level 14 mob, we have XP = (14 * 5 + 45) * (1 - 6/11) =  52.3 rounded to  52 XP.  

For a full table valid before Patch 4.03a, complete with in-game observed values, see: Greenman's Solo XP Table

For a basic spreadsheet valid before Patch 4.03a that performs these calculations, see: Solo Experience Google Spreadsheet
Elite Mobs

Mobs that are flagged as elite will give twice the amount of experience as a normal mob for the same level.

Elite XP = 2 * XP

The API UnitClassification function can return 5 levels of mob status: "worldboss", "rareelite", "elite", "rare", and "normal". Mobs classified as "elite" gain the 2x bonus as described. Mobs classified as "normal" and "rare" do not. The other two classes are unknown at this time.

Mobs in 5-man instances give slightly more XP, but mobs in some raid instances give no XP. Raid instances that DO award XP include Upper Blackrock Spire, Zul'Gurub and Karazhan.

(elites in instances give 2.5 * XP when solo-ed; untested for normal mobs in instances - Stilpu) (confirmed zero XP at lv 58 in Molten Core and Zul'Aman. confirmed XP gain in Zul'Gurub when a lv 56 dinged 57 but exact amount earned is unknown - Ishotala) Confirmed XP gain for Lvl 74 in Karazhan - cpuri (Some monsters, like in the Amphitheater of Anguish, give the doubled exp PER player. If you do those quests with less than five players, you'll get more experience than this formula. The total amount of exp they deliver is 10 * normal_XP. Has to be confirmed, through. pikachuyann)
Rested Bonus

The rested bonus doubles the experience given by a mob.

Rested XP = 2 * XP

This bonus is often reported as 50%. Consider this example for clarification:

a Level 11 rogue, fighting a level 10 Forest Lurker.  
Basic XP for this level combination is 86 points.  
The rogue is fully rested.  
Combat log shows: "Forest Lurker dies, you gain 172 experience (+86exp Rested bonus)"  
The experience bar shows that the rogue gained a total of 172 XP.  
So the XP gained was 86 (Base XP) + 86 (Rested Bonus)  

The bonus is not 50% of the base XP, it is 50% of the total XP gained.

The bonus is not 33% of the total gained, that is, the rogue above did not gain 172 + 86 XP total.

The rested bonus is accumulated as a certain number of future XP points. Your bonus on a kill will never exceed the amount of future XP points stored. Thus, if you have no rested bonus, and you stay briefly at an inn you will only accumulate 1 or 2 points. On your first kill thereafter, your Rested bonus will be that 1 or 2 points, not the full 100% bonus.
Solo Experience Modifiers

The experience amounts given above assume that the solo character and their pets have done 100% of the damage to kill the mob. Experience will be reduced when:

    Someone else (ungrouped) helps to damage the mob. The XP you receive depends on if that someone else will receive xp for killing that mob. If yes, you get full XP. If not, you get a tiny fraction. It doesn't matter how many other ungrouped help damage nor how much damage you done.

Example: You're 6th level fighting a 6th level mob. If the ungrouped helper is level 10 or lower damage the mob, you will get full XP because a character level 10 or lower can receive XP from a 6th level mob. If the ungrouped helper is level 11 or higher, you get a tiny fraction of the XP.

    Someone else (ungrouped) helps you by healing you in mid fight. This will cost you only a few XP points per heal.
    Your damage shields do significant damage to the mob. This is usually a small loss, 5-10 XP points at worst.
    The mob's maximum health is reduced below its current health

The effect of this is that power-leveling low levels is much less effective than at higher levels. It is faster for low levels to kill without help, than it is to powerlevel them. It also makes it hard to determine the solo XP for high level mobs, since you won't get an accurate number unless the character does 100% of the damage.

The way to power-level is to be near the character level you're power-leveling. Thus, a 49th character will be much more effective than a 70th character in power-leveling a 40th level character. More effective would be multiple 49th characters power-leveling the 40th level character.
Level 60 and Up Characters

The Burning Crusade expansion raised the level cap from 60 to 70 when it was released. The Gray Level for a level 60 character changed from the previously known 47 to 51. The Zero Difference value at level 60 is indeed 17, and remains there up to level 69.

The Wrath of the Lich King expansion raised the level cap from 70 to 80 when it was released. The Gray Level and Zero Difference values did not change from their 60-69 values. The only significant change is the zone modifier was raised from 235 to 580 for kills made in Northrend zones.
Group Experience

The following is after very limited testing. The formulae here are best guess.

Assuming everyone in the group is the same level

XP = MXP/numberOfMembers * modifier.

Modifiers:

1 person = 1.0  
2 person group = 1.0  
3 person group = 1.166  
4 person group = 1.3  
5 person group = 1.4  

Example:

1 person = 100xp  
2 people = 50xp each.  
3 people = ~39xp each.  
4 people = ~33xp each.  
5 people = ~28xp each.  

Raid-sized groups dramatically decrease experience gained (the party bonus does not apply).
Two Character Groups

    this information is based on a few limited trials

In a group of two characters, the total experience awarded for the mob is calculated using the solo XP formula and the level of the higher character. The experience is always divided between the two characters in the same ratio, regardless of how much damage was caused by each. The ratio for each character is determined by the formula:

CL1 = Character 1 Level, assumed to be the higher level character  
CL2 = Character 2 Level  
MXP = Solo Mob Experience, as calculated above for CL1  
XP1 = MXP * CL1 / (CL1 + CL2),   the experience awarded to character 1  
XP2 = MXP * CL2 / (CL1 + CL2),   the experience awarded to character 2  

Note that XP1 and XP2 appear to be rounded to an integer value. The rested bonus available to each character is then applied based on the rounded amount of XP.

There is no group XP bonus for groups of only two players.

Examples:

A duo of levels 35 and 34 kill a mob of level 30.  
CL1 = 35  
CL2 = 34  
ML  = 30  
MXP = 128   (the solo XP as calculated for the level 35 char)  
XP1 = 128 * 35 / 69 = 64.9275 (rounds up to 65)   
XP2 = 128 * 34 / 69 = 63.0725 (rounds down to 63)  

A duo of levels 36 and 34 kill a mob of level 34.  
CL1 = 36  
CL2 = 34  
ML  = 34  
MXP = 187.500   (the solo XP as calculated for the level 36 char)  
XP1 = 187.5 * 36 / 70 = 96.429 (rounds down to 96)  
XP2 = 187.5 * 34 / 70 = 91.071 (rounds down to 91)  

When the mob is gray to the higher level character but not to the lower, the higher character will get no XP. The lower character will get some small amount of XP. (No idea how this is calculated or scales at this time. However, it does depend on the higher level's level. Ex: a level 70 grouped with a level 15 kills a level 20 mob. The level 15 gets less xp than if the higher level was a level 40)

    Amendment by Xist on 07/20/2008

I have leveled one hunter from 35 to 41 (so far) with a level 70 hunter. The best that I can tell the formula is pretty much the same as with any group, only halved, although I consistently receive one single experience point more than I would expect, so that a level thirty-five alone against a same-level elite would receive:

2 * ((5 * 35) + 45)

or 440 experience. However, when grouped with a seventy, it gets divided:

((2 * (5 * 35) + 45) * 35 / (35 + 70))/2 + 1

    end of amendment

    Amendment by TomWolf on 10/14/2008

Trials (the day before patch 3 drops) shows that the elites in instances have a modification of 2.75.

Formula is now: ((2.75 * (basexp * levelmodification) * charlevel / (charlevel + 70))/2 + 1

    end of amendment

Example Code

LUA function for calculating xp based on the above figures

```lua
function CalcXp()
      t = UnitLevel("target");
      p = UnitLevel("player");
      if ( t == -1 ) then
           return 0;
      end
      if ( t == p ) then
           xp = ((p * 5) + 45);
      end
      if ( t > p ) then
           xp = ((p * 5) + 45) * (1 + 0.05 * (t - p));
      end
      if ( t < p ) then
           -- need gray level "g"
           if (p < 6) then g = 0; end
           if (p > 5 and p < 40) then
                g = p - 5 - floor(p/10);
           end
           if (p > 39) then
                g = p - 1 - floor(p/5);
           end
           if (t > g) then
                -- need zero difference "z"
                if (p < 8) then z = 5; end
                if (p > 7 and p < 10) then z = 6; end
                if (p > 9 and p < 12 ) then z = 7; end
                if (p > 11 and p < 16 ) then z = 8; end
                if (p > 15 and p < 20 ) then z = 9; end
                if (p > 19 and p < 40 ) then z = 9 + floor(p/10); end
                if (p > 39) then z = 5 + floor(p/5); end
                xp = (p * 5 + 45) * (1 - (p - t) / z);
           else 
                -- t <= g, mob is Gray
                xp = 0;
           end
      end
      xp = floor(xp+0.5);    -- result is rounded before calculating rest bonus
      if ( GetRestState() == 1) then
           xp = xp * 2;
      end
      if ( UnitClassification("target") == "elite" ) then
           xp = xp * 2;
           -- what about "worldboss", "rareelite"... not sure how the XP scales
      end
      if (xp > 0) then
           return xp;
      else
           return 0;
      end
end
```

These are C++ functions for calculating any mob XP (this is from my experimentation with a Palm OS WoW-like game, from which I 'stole' most of the formulae). They also include the mob difficulty colors (referred to as ConColors).

```C++

// Mob XP Functions (including Con Colors)
// Colors will be numbers:
//  {grey = 0, green = 1, yellow = 2, orange = 3, red = 4, skull = 5}
// NOTE: skull = red when working with anything OTHER than mobs!

double getConColor(int playerlvl, int moblvl)
{
	if(playerlvl + 5 <= moblvl) {
		if(playerlvl + 10 <= moblvl) {
			return 5;
		}
		else {
			return 4;
		}
	}
	else {
		switch(moblvl - playerlvl)
		{
			case 4:
			case 3:
			return 3;
			break;
			case 2:
			case 1:
			case 0:
			case -1:
			case -2:
			return 2;
			break;
			default:
			// More adv formula for grey/green lvls:
			if(playerlvl <= 5) {
				return 1; //All others are green.
			}
			else {
				if(playerlvl <= 39) {
					if(moblvl <= (playerlvl - 5 - floor(playerlvl/10))) {
						// Its below or equal to the 'grey level':
						return 0;
					}
					else {
						return 1;
					}
				}
				else {
					//player over lvl 39:
					if(moblvl <= (playerlvl - 1 - floor(playerlvl/5))) {
						return 0;
					}
					else {
						return 1;
					}
				}
			}
		}
	}
}

int getZD(int lvl)
{
	if(lvl <= 7) {
		return 5;
	}
	if(lvl <= 9) {
		return 6;
	}
	if(lvl <= 11) {
		return 7;
	}
	if(lvl <= 15) {
		return 8;
	}
	if(lvl <= 19) {
		return 9;
	}
	if(lvl <= 29) {
		return 11;
	}
	if(lvl <= 39) {
		return 12;
	}
	if(lvl <= 44) {
		return 13;
	}
	if(lvl <= 49) {
		return 14;
	}
	if(lvl <= 54) {
		return 15;
	}
	if(lvl <= 59) {
	 	return 16;
	}
	else {
	 	return 17; // Approx.
	}
}

double getMobXP(int playerlvl, int moblvl)
{
	if(moblvl >= playerlvl) {
		double temp = ((playerlvl * 5) + 45) * (1 + (0.05 * (moblvl - playerlvl)));
		double tempcap = ((playerlvl * 5) + 45) * 1.2;
		if(temp > tempcap) {
			return floor(tempcap);
		}
		else {
			return floor(temp);
		}
	}
	else {
		if(getConColor(playerlvl, moblvl) == 0) {
			return 0;
		}
		else {
			return floor((playerlvl * 5) + 45) * (1 - (playerlvl -  moblvl)/getZD(playerlvl));
		}
	}
}
double getEliteMobXP(int playerlvl, int moblvl)
{
	return getMobXP(playerlvl, moblvl) * 2;
}

// Rested Bonus:
// Restedness is double XP, but if we only have part restedness we must split the XP:

double getMobXPFull(int playerlvl, int moblvl, bool elite, int rest)
{
	// rest = how much XP is left before restedness wears off:
	double temp = 0;
	if(elite) {
		temp = getEliteMobXP(playerlvl, moblvl);
	}
	else {
		temp = getMobXP(playerlvl, moblvl);
	}
	// Now to apply restedness.  temp = actual XP.
	// If restedness is 0...
	if(rest == 0) {
		return temp;
	}
	else {
		if(rest >= temp) {
			return temp * 2;
		}
		else {
			//Restedness is partially covering the XP gained.
			// XP = rest + (AXP - (rest / 2))
			return rest + (temp - (rest / 2));
		}
	}
}
// Party Mob XP:
double getPartyMobXPFull(int playerlvl, int highestlvl, int sumlvls, int moblvl, bool elite, int  rest)
{
	double temp = getMobXPFull(highestlvl, moblvl, elite, 0);
	// temp = XP from soloing via highest lvl...
	temp = temp * playerlvl / sumlvls;
	if(rest == 0) {
		return temp;
	}
	else {
		if(rest >= temp) {
			return temp * 2;
		}
		else {
			//Restedness is partially covering the XP gained.
			// XP = rest + (AXP - (rest / 2))
			return rest + (temp - (rest / 2));
		}
	}
}

```

Here are some functions to do these calculations in PHP. Only single person XP is provided.

```php
<?
	function calculateXP($playerlevel, $moblevel, $elite = false, $rested = false)
	{
		if($playerlevel < $moblevel)
		{
			if($moblevel - $playerlevel > 4)
				$moblevel = $playerlevel + 4;
			$xp = ($playerlevel * 5 + 45) * (1 + 0.05 * ($moblevel - $playerlevel));
		}
		elseif($playerlevel == $moblevel)
		{
			$xp = $playerlevel * 5 + 45;
		}
		elseif($playerlevel > $moblevel)
		{
			if($moblevel <= calculateGrayLevel($playerlevel))
				$xp = 0;
			else
			{
				if($playerlevel >= 1 && $playerlevel <= 7)
					$zd = 5;
				elseif($playerlevel >= 8 && $playerlevel <= 9)
					$zd = 6;
				elseif($playerlevel >= 10 && $playerlevel <= 11)
					$zd = 7;
				elseif($playerlevel >= 12 && $playerlevel <= 15)
					$zd = 8;
				elseif($playerlevel >= 16 && $playerlevel <= 19)
					$zd = 9;
				elseif($playerlevel >= 20 && $playerlevel <= 29)
					$zd = 11;
				elseif($playerlevel >= 30 && $playerlevel <= 39)
					$zd = 12;
				elseif($playerlevel >= 40 && $playerlevel <= 44)
					$zd = 13;
				elseif($playerlevel >= 45 && $playerlevel <= 49)
					$zd = 14;
				elseif($playerlevel >= 50 && $playerlevel <= 54)
					$zd = 15;
				elseif($playerlevel >= 55 && $playerlevel <= 59)
					$zd = 16;
				elseif($playerlevel == 60)
					$zd = 17;
				$xp = ($playerlevel * 5 + 45) * (1 - ($playerlevel - $moblevel) / $zd);
			}
		}
		if($elite == true)
			$xp *= 2;
		if($rested == true)
			$xp *= 2;
		return round($xp);
	}
	function calculateGrayLevel($playerlevel)
	{
		if($playerlevel >= 1 && $playerlevel <= 5)
			return 0;
		elseif($playerlevel >= 6 && $playerlevel <= 39)
			return $playerlevel - 5 - floor($playerlevel / 10);
		elseif($playerlevel >= 40 && $playerlevel <= 60)
			return $playerlevel - 1 - floor($playerlevel / 5);
	}
	function calculateDifficulty($playerlevel, $moblevel)
	{
		$leveldiff = $moblevel - $playerlevel;
		if($moblevel <= calculateGrayLevel($playerlevel))
			return 'Gray';
		else
		{
			if($leveldiff >= 5)
				return 'Red';
			elseif($leveldiff >= 3 && $leveldiff <= 4)
				return 'Orange';
			elseif($leveldiff >= -2 && $leveldiff <= 2)
				return 'Yellow';
			elseif($leveldiff <= -3)
				return 'Green';
		}
	}
?>
```


