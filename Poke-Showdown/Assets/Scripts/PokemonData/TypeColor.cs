using System.Collections.Generic;
using UnityEngine;

namespace PokemonData
{
    public static class TypeColor
    {
        public static readonly IDictionary<Type, Color> TypeColorDict = new Dictionary<Type, Color>()
        {
            {Type.Normal, new Color(170 / 255f, 170 / 255f, 153 / 255f)},
            {Type.Fire, new Color(255 / 255f, 68 / 255f, 34 / 255f)},
            {Type.Water, new Color(51 / 255f, 153 / 255f, 255 / 255f)},
            {Type.Electric, new Color(255 / 255f, 204 / 255f, 51 / 255f)},
            {Type.Grass, new Color(119 / 255f, 204 / 255f, 85 / 255f)},
            {Type.Ice, new Color(102 / 255f, 204 / 255f, 255 / 255f)},
            {Type.Fighting, new Color(187 / 255f, 85 / 255f, 68 / 255f)},
            {Type.Poison, new Color(170 / 255f, 85 / 255f, 153 / 255f)},
            {Type.Ground, new Color(221 / 255f, 187 / 255f, 85 / 255f)},
            {Type.Flying, new Color(136 / 255f, 153 / 255f, 255 / 255f)},
            {Type.Psychic, new Color(255 / 255f, 85 / 255f, 153 / 255f)},
            {Type.Bug, new Color(170 / 255f, 187 / 255f, 34 / 255f)},
            {Type.Rock, new Color(187 / 255f, 170 / 255f, 102 / 255f)},
            {Type.Ghost, new Color(102 / 255f, 102 / 255f, 187 / 255f)},
            {Type.Dragon, new Color(119 / 255f, 102 / 255f, 238 / 255f)},
            {Type.Dark, new Color(119 / 255f, 85 / 255f, 68 / 255f)},
            {Type.Steel, new Color(170 / 255f, 170 / 255f, 187 / 255f)},
            {Type.Fairy, new Color(238 / 255f, 153 / 255f, 238 / 255f)},
        };
    }
}
