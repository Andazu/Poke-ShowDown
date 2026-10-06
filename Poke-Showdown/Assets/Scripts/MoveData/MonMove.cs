using PokemonData;

namespace MoveData
{
    public enum MoveClass
    {
        Phsyical,Special,Status
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
    }
}