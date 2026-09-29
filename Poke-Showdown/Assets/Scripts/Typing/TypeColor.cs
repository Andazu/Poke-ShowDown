using System.Collections.Generic;
using Battles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Typing
{
    public static class TypeColor
    {
        public static readonly IDictionary<string, Color> TypeColorDict = new Dictionary<string, Color>()
        {
            {"normal", new Color(170 / 255f, 170 / 255f, 153 / 255f)},
            {"fire", new Color(255 / 255f, 68 / 255f, 34 / 255f)},
            {"water", new Color(51 / 255f, 153 / 255f, 255 / 255f)},
            {"electric", new Color(255 / 255f, 204 / 255f, 51 / 255f)},
            {"grass", new Color(119 / 255f, 204 / 255f, 85 / 255f)},
            {"ice", new Color(102 / 255f, 204 / 255f, 255 / 255f)},
            {"fighting", new Color(187 / 255f, 85 / 255f, 68 / 255f)},
            {"poison", new Color(170 / 255f, 85 / 255f, 153 / 255f)},
            {"ground", new Color(221 / 255f, 187 / 255f, 85 / 255f)},
            {"flying", new Color(136 / 255f, 153 / 255f, 255 / 255f)},
            {"psychic", new Color(255 / 255f, 85 / 255f, 153 / 255f)},
            {"bug", new Color(170 / 255f, 187 / 255f, 34 / 255f)},
            {"rock", new Color(187 / 255f, 170 / 255f, 102 / 255f)},
            {"ghost", new Color(102 / 255f, 102 / 255f, 187 / 255f)},
            {"dragon", new Color(119 / 255f, 102 / 255f, 238 / 255f)},
            {"dark", new Color(119 / 255f, 85 / 255f, 68 / 255f)},
            {"steel", new Color(170 / 255f, 170 / 255f, 187 / 255f)},
            {"fairy", new Color(238 / 255f, 153 / 255f, 238 / 255f)},
        };
    }
}
