using System.Linq;
using MoveData;
using PokemonData;
using UnityEngine;

namespace Battles
{
    public static class DamageCalculator
    {
        public enum Effectiveness
        {
            SuperEffective,Regular,NotEffective,NoEffect
        }
        
        private static float CalculateEffectivenessMultiplier(PokemonData.Type moveType, PokemonData.Type[] defenderTypes)
        {
            var effectiveness = 1f;
            if (TypeEffectiveness.NoEffect.TryGetValue(moveType, out var immunes))
            {
                if (immunes.Contains(defenderTypes[0]) || immunes.Contains(defenderTypes[1]))
                {
                    return 0;
                }
            }

            var superEffectives = TypeEffectiveness.SuperEffective[moveType];
            var resists = TypeEffectiveness.Resistances[moveType];
            foreach (var type in defenderTypes)
            {
                if (superEffectives.Contains(type))
                {
                    effectiveness *= 2;
                }else if (resists.Contains(type))
                {
                    effectiveness /= 2;
                }
            }
            return effectiveness;
        }
        
        public static (int,Effectiveness,bool) CalculateDamage(Pokemon attacker, Pokemon defender, MonMove move)
        {
            //(⌊⌊(⌊2×Level5⌋+2)×Power×A/D⌋50⌋+2)×Targets×PB×Weather×GlaiveRush×Critical×random×STAB×Type×Burn×other×ZMove×TeraShield
            var effect = CalculateEffectivenessMultiplier(move.type, new[]{defender.type1, defender.type2});

            if (effect == 0.0)
            {
                return (0, Effectiveness.NoEffect,false);
            }

            var effectiveness = Effectiveness.Regular;
            effectiveness = effect >= 2 ? Effectiveness.SuperEffective : effectiveness;
            effectiveness = effect <= 0.5f ? Effectiveness.NotEffective : effectiveness;

            var tmp = Mathf.FloorToInt(2 * attacker.level / 5.0f)+2;

            var aOverD = move.moveClass == MoveClass.Phsyical
                ? attacker.stats[1] / (float)defender.stats[2]
                : attacker.stats[3] / (float)defender.stats[4];

            var target = move.targets >= 2 ? 0.75f : 1;

            var crit = Random.Range(0, 1.0f) <= 1 / 24.0f ? 1.5f : 1;

            var stab = attacker.type1 == move.type || attacker.type2 == move.type ? 1.5f : 1;

            var rand = Random.Range(85, 101);

            var dmg = (2 + Mathf.FloorToInt((tmp * move.power * aOverD) / 50.0f)) * target * crit * stab*effect;
            dmg = Mathf.FloorToInt(dmg) * target;
            dmg = Mathf.FloorToInt(dmg) * crit;
            dmg = Mathf.FloorToInt(Mathf.FloorToInt(dmg) * rand/100.0f);
            dmg = Mathf.FloorToInt(dmg) * stab;
            dmg = Mathf.FloorToInt(dmg) * effect;
            return (Mathf.FloorToInt(dmg),effectiveness,crit > 1);
        }
    }
}