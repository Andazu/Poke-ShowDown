using System;
using Audio;
using Battles;
using TMPro;
using UnityEngine;

namespace BattleUI
{
    public class HumanPlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private SelectBattleDialogue battleDialogue;
        
        public void EffectivenessText(DamageCalculator.Effectiveness  effectiveness)
        {
            switch (effectiveness)
            {
                case DamageCalculator.Effectiveness.SuperEffective:
                    dialogueText.text = "It's super effective!";
                    break;
                case DamageCalculator.Effectiveness.Regular:
                    break;
                case DamageCalculator.Effectiveness.NotEffective:
                    dialogueText.text = "It's not very effective..."; 
                    break;
                case DamageCalculator.Effectiveness.NoEffect:
                    dialogueText.text = "It has no effect";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effectiveness), effectiveness, null);
            }
        }

        public void MissedText(bool p1Missed, string monName)
        {
            if (p1Missed) 
            {
                dialogueText.text = monName+ " missed!";
            }
            else
            {
                dialogueText.text = "The foe's " + monName+ " missed!";
            }
        }

        public void CritText()
        {
            dialogueText.text = "A critical hit!";
        }
        
        public void SetVictoryText()
        {
            dialogueText.text = "You defeated the enemy trainer!";
        }
        
        public void SetLossText()
        {
            dialogueText.text = "You lost";
        }
    }
}