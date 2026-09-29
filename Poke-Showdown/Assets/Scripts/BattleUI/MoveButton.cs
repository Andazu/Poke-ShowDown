using Battles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Typing;

namespace BattleUI
{
    public class MoveButton : MonoBehaviour
    {
        private string _moveType;
        
        [SerializeField] private TextMeshProUGUI typeInfoText;
        private Button button;
        private ColorBlock color;

        public void SetType(string moveType)
        {
            _moveType = moveType;
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
            typeInfoText.text = _moveType.ToUpper();
            typeInfoText.color = TypeColor.TypeColorDict[_moveType];
        }

        public void ExitHover()
        {
            typeInfoText.text = "";
        }
    }
}