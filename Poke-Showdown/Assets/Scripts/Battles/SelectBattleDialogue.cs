using System.Collections;
using TMPro;
using UnityEngine;

namespace Battles
{
    public class SelectBattleDialogue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI commandText;

        [SerializeField] private buttonChoose displayMoves;

        [SerializeField] private AudioSource battleSelectSound;

        [Header("Overlays")]
        [SerializeField] private GameObject commandOverlay;
        [SerializeField] private GameObject button;
        [SerializeField] private GameObject movesOverlay;


        public void InitMoveMenu(Battle ongoing,Moves moves)
        {
            displayMoves.Initialize(ongoing, moves);
        }
        
        public void WhatWillPokeDo(string pokeName)
        {
            movesOverlay.gameObject.SetActive(false);
            button.gameObject.SetActive(true);
            commandOverlay.gameObject.SetActive(true);
            commandText.text = "What will " + pokeName + " do?";

        }

        public void MoveSelection()
        {
            StartCoroutine(DisableStartMenu());
        }

        IEnumerator DisableStartMenu()
        {
            yield return new WaitForSeconds(1f);
            commandOverlay.gameObject.SetActive(false);
            gameObject.SetActive(false);
            movesOverlay.gameObject.SetActive(true);
            displayMoves.MoveDisplay();
        }
        public void SelectionSound()
        {
            battleSelectSound.Play();
        }
 
    }
}
