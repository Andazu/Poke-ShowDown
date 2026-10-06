using UnityEngine;

namespace PokemonData
{
    public enum MoveClass
    {
        Phsyical,Special,Status
    }
    public struct Pokemon
    {
        public MonMove[] monMoves { get; set; }
        public int[] stats { get; set; } // hp, atk, def, sp.atk, sp.def, spe
        public MonAbitity ability { get; set; }
        public Type type1 { get; set; }
        public Type type2 { get; set; }
        public int level { get; set; }
        public int currentHp { get; set; }
        public byte gender { get; set; }

        //Stat calculations do not account for natures or EV's, and considers IV to always be 15
        public static int CalculateHp(int baseStat, int level)
        {
            return Mathf.FloorToInt(((2 * baseStat + 15) * level) / 100.0f) + level + 10;
        }
        public static int CalculateOtherStat(int baseStat, int level)
        {
            return Mathf.FloorToInt(((2 * baseStat + 15) * level) / 100.0f) + 5;
        }
    }

    public struct MonMove
    {
        public string moveName { get; set; }
        public Type type;
        public MoveClass moveClass;
        public int power;
        public int pp;
        public int currentPp;
        public int priority;
        public int targets;
    }

    public struct MonAbitity
    {
        public string name;
    }
}