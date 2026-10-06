using Battles;
using TMPro;
using UnityEngine;

namespace BattleUI
{
    public class ButtonChoose : MonoBehaviour
    {
        private Battle _battle;
        private Moves _p1Moves;

        [SerializeField] private TextMeshProUGUI move1;
        [SerializeField] private TextMeshProUGUI move2;
        [SerializeField] private TextMeshProUGUI move3;
        [SerializeField] private TextMeshProUGUI move4;
        [SerializeField] private AudioSource selectionSound;
        [SerializeField] private MoveButton[] moveButtons;
        


        public void Initialize(Battle battle, Moves p1Moves)
        {
            _battle = battle;
            _p1Moves = p1Moves;
            
            /*for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetType(_p1Moves.moveSet[i].type.name);
            }*/
        }

        public void MoveSelect1()
        {
            selectionSound.Play();
            MoveSelect(0);
        }
        public void MoveSelect2()
        {
            selectionSound.Play();
            MoveSelect(1);
        }
        public void MoveSelect3()
        {
            selectionSound.Play();
            MoveSelect(2);

        }
        public void MoveSelect4()
        {
            selectionSound.Play();
            MoveSelect(3);
        }

        void MoveSelect(int moveNum)
        {
            gameObject.SetActive(false);
            _battle.ChooseMove(moveNum);
        }

        public void MoveDisplay()
        {
            Debug.Log("WORKING");

            string move1Cap = char.ToUpper(_p1Moves.moveSet[0].moveName[0]) + _p1Moves.moveSet[0].moveName.Substring(1);
            move1.text = move1Cap;


            string move2Cap = char.ToUpper(_p1Moves.moveSet[1].moveName[0]) + _p1Moves.moveSet[1].moveName.Substring(1);
            move2.text = move2Cap;


            string move3Cap = char.ToUpper(_p1Moves.moveSet[2].moveName[0]) + _p1Moves.moveSet[2].moveName.Substring(1);
            move3.text = move3Cap;


            string move4Cap = char.ToUpper(_p1Moves.moveSet[3].moveName[0]) + _p1Moves.moveSet[3].moveName.Substring(1);
            move4.text = move4Cap;

        }
    }
}

