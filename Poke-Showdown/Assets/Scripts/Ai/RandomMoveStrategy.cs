using MoveData;
using PokemonData;
using UnityEngine;

namespace Ai
{
    public class RandomMoveStrategy : IMoveChooseStrategy
    {
        public (MonMove, Pokemon, int) ExecuteMoveChooseStrategy(Pokemon pokemon, int idxInBattle, int numBattlers)
        {
            var target = Random.Range(0, numBattlers);
            while (target == idxInBattle)
            {
                target = Random.Range(0, numBattlers);
            }
            var move = pokemon.monMoves[Random.Range(0, pokemon.monMoves.Length)];
            
            return (move, pokemon, target);
        }
    }
}