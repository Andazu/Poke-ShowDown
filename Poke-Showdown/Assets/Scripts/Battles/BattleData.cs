using PokeStatus;

namespace Battles
{

    public struct StatChanges
    {
        public int[] Changes; // atk, def, sp.atk, sp.def, spe, acc, eva
    }
    public struct BattleData
    {
        public (int, DamageCalculator.Effectiveness, bool) latestMoveResult;
        public TrainerTeam[] Trainers;

        public StatChanges[] StatChanges;
        //if we have time: global field effects eg. weather, terrain
        //if we have even more time: per side field effects eg. screens, hazards
    }
}