using PokemonData;

namespace MoveData
{
    public enum MoveClass
    {
        Phsyical,Special,Status
    }

    public enum MoveEffect
    {
        DealDamage, KillSelf, RaiseStatAtk, RaiseStatDef, RaiseStatSpDef, RaiseStatSpAtk, RaiseStatSpeed, RaiseStatEvasion,
        RaiseStatAcc, RaiseStatCrit, FlinchTarget, SetWeatherRain, SetWeatherSun, SetWeatherSand
        
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
        public int accuracy;
        public MoveEffect[] effects;
    }
}