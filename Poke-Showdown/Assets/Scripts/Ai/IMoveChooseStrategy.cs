using MoveData;
using PokemonData;

namespace Ai
{
    public interface IMoveChooseStrategy
    {
        public (MonMove,Pokemon,int) ExecuteMoveChooseStrategy(Pokemon pokemon, int idxInBattle, int numBattlers);
    }
}