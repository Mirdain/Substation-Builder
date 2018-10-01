using System;
using System.Linq;

namespace Substation_Builder.Resources.Monster
{
    static public class Monster
    {
        static string[] Prefix = 
        {
        "Gloom",
        "Gray",
        "Dire",
        "Black",
        "Shadow",
        "Haze",
        "Wind",
        "Storm",
        "Warp",
        "Night",
        "Moon",
        "Star",
        "Pit",
        "Fire",
        "Cold",
        "Seethe",
        "Sharp",
        "Ash",
        "Blade",
        "Steel",
        "Stone",
        "Rust",
        "Mold",
        "Blight",
        "Plague",
        "Rot",
        "Ooze",
        "Puke",
        "Snot",
        "Bile",
        "Blood",
        "Pulse",
        "Gut",
        "Gore",
        "Flesh",
        "Bone",
        "Spine",
        "Mind",
        "Spirit",
        "Soul",
        "Wrath",
        "Grief",
        "Foul",
        "Vile",
        "Sin",
        "Chaos",
        "Dread",
        "Doom",
        "Bane",
        "Death",
        "Viper",
        "Dragon",
        "Devil",
        };

        static string[] Suffix = 
        {
        "Touch",
        "Spell",
        "Feast",
        "Wound",
        "Grin",
        "Maim",
        "Hack",
        "Bite",
        "Rend",
        "Burn",
        "Rip",
        "Kill",
        "Call",
        "Vex",
        "Jade",
        "Web",
        "Shield",
        "Killer",
        "Razor",
        "Drinker",
        "Shifter",
        "Crawler",
        "Dancer",
        "Bender",
        "Weaver",
        "Eater",
        "Widow",
        "Maggot",
        "Spawn",
        "Wight",
        "Grumble",
        "Growler",
        "Snarl",
        "Wolf",
        "Crow",
        "Hawk",
        "Cloud",
        "Bang",
        "Head",
        "Skull",
        "Brow",
        "Eye",
        "Maw",
        "Tongue",
        "Fang",
        "Horn",
        "Thorn",
        "Claw",
        "Fist",
        "Heart",
        "Shank",
        "Skin",
        "Wing",
        "Pox",
        "Fester",
        "Blister",
        "Pus",
        "Slime",
        "Drool",
        "Froth",
        "Sludge",
        "Venom",
        "Poison",
        "Break",
        "Shard",
        "Flame",
        "Maul",
        "Thirst",
        "Lust",
        };

        static string[] Appelation = 
        {
        "the Hammer",
        "the Axe",
        "the Sharp",
        "the Jagged",
        "the Flayer",
        "the Slasher",
        "the Impaler",
        "the Hunter",
        "the Slayer",
        "the Mauler",
        "the Destroyer",
        "the Quick",
        "the Witch",
        "the Mad",
        "the Wraith",
        "the Shade",
        "the Dead",
        "the Unholy",
        "the Howler",
        "the Grim",
        "the Dark",
        "the Tainted",
        "the Unclean",
        "the Hungry",
        "the Cold",
        "the Evil",
        "the Mad",
        "the Dangerous",
        "the Ghostly",
        "the Doctor",
        "the Crazy",
        "the Bloody",
        "the Rancid",
        "the Dark",
        "the Phantom",
        "the Unstoppable"
        };


        // Example

        //Snot, thirst, the Unclean, - ,Champion, of Engineers
        static string[] Level = 
        {
        "Ruler",
        "Leader",
        "Champion",
        "Savior",
        "Crusader",
        "Hero",
        "King",
        "Queen",
        "Prince",
        "Princess",
        "Savior",
        };

        public static string[] MonsterName()
        {
            string MonsterLong = null;
            string MonsterFirst = null;
            string MonsterLast = null;
            string[] Monster = new string[3];

            Random rnd = new Random();
            int prefixid = rnd.Next(0, Prefix.Count());
            int suffixid = rnd.Next(0, Suffix.Count());
            int appelation = rnd.Next(0, Appelation.Count());
            int level = rnd.Next(0, Level.Count());

            MonsterLong = Prefix[prefixid] + " " + Suffix[suffixid] + " " + Appelation[appelation] + " - " + Level[level] + " of Engineers";

            MonsterFirst = Prefix[prefixid] + " " + Suffix[suffixid] + " " + Appelation[appelation];
            MonsterLast = Level[level] + " of Engineers";

            Monster[0] = MonsterFirst.ToUpper();
            Monster[1] = MonsterLast.ToUpper();
            Monster[2] = MonsterLong.ToUpper();

            return Monster;
        }

    }
}
