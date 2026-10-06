using System;
using PokemonData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Type = PokemonData.Type;

namespace BattleUI
{
    public class MoveButton : MonoBehaviour
    {
        private Type _moveType;
        private string _moveTypeName;
        
        [SerializeField] private TextMeshProUGUI typeInfoText;
        private Button button;
        private ColorBlock color;

        public void SetType(string moveType)
        {
            _moveTypeName = moveType;
            Enum.TryParse(moveType, out _moveType);
        }
        
        private void Awake()
        {
            button = GetComponent<Button>();
            color = button.colors;
        }
        
        public void HoverButton()
        {
            color.highlightedColor = TypeColor.TypeColorDict[_moveType]; //typeColor[string of type from move]
            button.colors = color;
            typeInfoText.text = _moveTypeName.ToUpper();
            typeInfoText.color = TypeColor.TypeColorDict[_moveType];
        }

        public void ExitHover()
        {
            typeInfoText.text = "";
        }
    }
}