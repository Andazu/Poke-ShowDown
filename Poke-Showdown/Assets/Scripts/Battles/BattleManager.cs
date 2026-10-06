using System.Collections;
using System.Linq;
using Ai;
using MoveData;
using PokemonData;
using UnityEngine;

namespace Battles
{
    public class BattleManager : MonoBehaviour
    {
        private BattleData _battleData;
        [SerializeField] private bool hasHumanPlayer;
        [SerializeField] private bool hasUI;

        private (MonMove, Pokemon, int)[] _chosenMoves; 
        private int _numTrainersFinishedChoosing;

        private bool _finishedTurn;

        private void Start()
        {
            _finishedTurn = true;
        }

        private void Update()
        {
            if (_finishedTurn)
            {
                StartCoroutine(HandleTurn());
            }
        }

        private IEnumerator HandleTurn()
        {
            _chosenMoves = new (MonMove, Pokemon, int)[_battleData.Trainers.Length]; // move,speed of pokemon using move
            _numTrainersFinishedChoosing = 0;
            for (var i = 0; i < _chosenMoves.Length; i++)
            {
                if (hasHumanPlayer && i == 1)
                {
                    StartCoroutine(ChooseHumanMove(_battleData.Trainers[i].activePokemon, i));
                    continue;
                }
                StartCoroutine(ChooseMove(_battleData.Trainers[i].activePokemon,i
                    , new RandomMoveStrategy()));
            }

            // Wait until all trainers have chosen a move
            while (_numTrainersFinishedChoosing < _battleData.Trainers.Length)
            {
                yield return null;
            }

            var ordered = _chosenMoves.OrderBy(m => m.Item1.priority)
                .ThenBy(m => m.Item2.stats[5]).ToArray();
            
            // play moves in order of prio/speed, change text and play sound after each move if necessary,
            // Calculate damage and update health bars
            foreach (var (move,pokemon,target) in ordered)
            {
                MoveResolver.ResolveMove(move,pokemon,_battleData,target);
                if (hasUI)
                {
                    // add handling for ui and sound
                }
            }
            
            // End of turn effects
            
        }

        private IEnumerator ChooseHumanMove(Pokemon pokemon, int index)
        {
            var moveChosen = false;
            while (!moveChosen)
            {
                
                yield return null;
            }
        }

        private IEnumerator ChooseMove(Pokemon pokemon,int idx, IMoveChooseStrategy strategy)
        {
            _chosenMoves[idx] = strategy.ExecuteMoveChooseStrategy(pokemon, idx,  _battleData.Trainers.Length);
            _numTrainersFinishedChoosing++;
            yield return null;
        }
        
    }
}