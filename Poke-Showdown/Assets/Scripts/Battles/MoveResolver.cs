using System;
using MoveData;
using PokemonData;

namespace Battles
{
    
    public static class MoveResolver
    {
        public static void ResolveMove(MonMove move, Pokemon pokemon, BattleData battleData, int targetIndexes)
        {
            foreach (var effect in move.effects)
            {
                switch (effect)
                {
                    case MoveEffect.DealDamage:
                        DealDamage(move, pokemon, battleData, targetIndexes);
                        break;
                    case MoveEffect.KillSelf:
                        break;
                    case MoveEffect.RaiseStatAtk:
                        break;
                    case MoveEffect.RaiseStatDef:
                        break;
                    case MoveEffect.RaiseStatSpDef:
                        break;
                    case MoveEffect.RaiseStatSpAtk:
                        break;
                    case MoveEffect.RaiseStatSpeed:
                        break;
                    case MoveEffect.RaiseStatEvasion:
                        break;
                    case MoveEffect.RaiseStatAcc:
                        break;
                    case MoveEffect.RaiseStatCrit:
                        break;
                    case MoveEffect.FlinchTarget:
                        break;
                    case MoveEffect.SetWeatherRain:
                    case MoveEffect.SetWeatherSun:
                    case MoveEffect.SetWeatherSand:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private static void DealDamage(MonMove move, Pokemon pokemon, BattleData battleData, int targetIndex)
        {
            battleData.latestMoveResult = DamageCalculator.CalculateDamage(pokemon, battleData.Trainers[targetIndex].activePokemon, move);
        }
    }
}