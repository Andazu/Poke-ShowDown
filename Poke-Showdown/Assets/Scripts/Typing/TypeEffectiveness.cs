using System;
using System.Collections.Generic;

namespace Typing
{
    public static class TypeEffectiveness
    {
        public static readonly IDictionary<string, string[]> superEffective = new Dictionary<string, string[]>()
        {
            {"normal", Array.Empty<string>() },{"fire",new []{"ice","grass","steel"}},{"water", new[] { "fire", "ground", "rock" }},
            {"electric", new[] { "water", "flying" }},{"grass", new[] { "water", "ground", "rock" }},{"ice", new[] { "grass", "ground", "flying", "dragon" }},
            {"fighting", new[] { "normal", "ice", "rock", "dark", "steel" }},{"poison", new[] { "grass", "fairy" }},
            {"ground", new[] { "fire", "electric", "poison", "rock", "steel" }},{"fairy", new[] { "fighting", "dragon", "dark" }},
            {"steel", new[] { "ice", "rock", "fairy" }},{"dark", new[] { "psychic", "ghost" }},{"dragon", new[] { "dragon" }},
            {"ghost", new[] { "psychic", "ghost" }},{"rock", new[] { "fire", "ice", "flying", "bug" }},{"bug", new[] { "grass", "psychic", "dark" }},
            {"psychic", new[] { "fighting", "poison" }},{"flying", new[] { "grass", "fighting", "bug" }}
        };
    
        public static readonly IDictionary<string, string[]> notEffective = new Dictionary<string, string[]>()
        {
            {"normal", new []{"rock","steel"}},{"fire", new[] { "fire", "water", "rock" }},{"water", new[] { "water", "grass", "dragon" }},
            {"electric", new[] { "electric", "grass", "dragon" }},{"grass", new[] { "fire", "grass", "poison", "flying", "bug", "dragon", "steel" }},
            {"ice", new[] { "fire", "water", "ice", "steel" }},{ "fighting", new[] { "poison", "flying", "psychic", "bug", "fairy" }},
            {"poison", new[] { "poison", "ground", "rock", "ghost" }},{"ground", new[] { "grass", "bug" }},
            {"fairy", new[] { "poison", "steel" }},{"steel", new[] { "fire", "fighting", "ground" }},
            {"dark", new[] { "fighting", "bug", "fairy" }},{"dragon", new[] { "ice", "dragon", "fairy" }},
            {"ghost", new[] { "ghost", "dark" }}, {"rock", new[] { "water", "grass", "fighting", "ground", "steel" }},
            {"bug", new[] { "fire", "flying", "rock" }}, {"psychic", new[] { "bug", "ghost", "dark" }},
            {"flying", new[] { "electric", "ice", "rock" }}
        };
        public static readonly IDictionary<string, string[]> noEffect = new Dictionary<string, string[]>()
        {
            {"normal", new[] { "ghost" }}, {"fire", new string[] {}}, {"water", new string[] {}},
            {"electric", new[] { "ground"}}, {"grass", new string[] {}}, {"ice", new string[] {}},
            {"fighting", new[] { "ghost" }}, {"poison", new[] { "steel" }}, {"ground", new[] { "flying" }},
            {"flying", new string[] { }}, {"psychic", new[] { "dark" }}, {"bug", new string[] { }},
            {"rock", new string[] { }}, {"ghost", new[] { "normal" }}, {"dragon", new[] { "fairy" }},
            {"dark", new string[] { }}, {"steel", new string[] { }}, {"fairy", new string[] { }}
        };
    }
}
