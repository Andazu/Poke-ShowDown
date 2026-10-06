using System;
using System.Collections.Generic;

namespace PokemonData
{
    public enum Type
    {
        Normal,Fire,Ice,Electric,Fighting,Water,Grass,Ground,Rock,Ghost
        ,Psychic,Poison,Steel,Dragon,Fairy,Flying,Dark,Bug,None
    }
    public static class TypeEffectiveness
    {
        public static readonly IDictionary<Type, Type[]> SuperEffective = new Dictionary<Type, Type[]>
        {
            {Type.Normal, Array.Empty<Type>() },{Type.Fire,new []{Type.Ice,Type.Grass,Type.Steel}},
            {Type.Water, new[] { Type.Fire, Type.Ground, Type.Rock }},
            {Type.Electric, new[] { Type.Water, Type.Flying }},{Type.Grass, new[] { Type.Water, Type.Ground, Type.Rock }},{Type.Ice, new[] { Type.Grass, Type.Ground, Type.Flying, Type.Dragon }},
            {Type.Fighting, new[] { Type.Normal, Type.Ice, Type.Rock, Type.Dark, Type.Steel }},{Type.Poison, new[] { Type.Grass, Type.Fairy }},
            {Type.Ground, new[] { Type.Fire, Type.Electric, Type.Poison, Type.Rock, Type.Steel }},{Type.Fairy, new[] { Type.Fighting, Type.Dragon, Type.Dark }},
            {Type.Steel, new[] { Type.Ice, Type.Rock, Type.Fairy }},{Type.Dark, new[] { Type.Psychic, Type.Ghost }},{Type.Dragon, new[] { Type.Dragon }},
            {Type.Ghost, new[] { Type.Psychic, Type.Ghost }},{Type.Rock, new[] { Type.Fire, Type.Ice, Type.Flying, Type.Bug }},{Type.Bug, new[] { Type.Grass, Type.Psychic, Type.Dark }},
            {Type.Psychic, new[] { Type.Fighting, Type.Poison }},{Type.Flying, new[] { Type.Grass, Type.Fighting, Type.Bug }}
        };
    
        public static readonly IDictionary<Type, Type[]> Resistances = new Dictionary<Type, Type[]>
        {
            {Type.Normal, new []{Type.Rock,Type.Steel}},{Type.Fire, new[] { Type.Fire, Type.Water, Type.Rock }},{Type.Water, new[] { Type.Water, Type.Grass, Type.Dragon }},
            {Type.Electric, new[] { Type.Electric, Type.Grass, Type.Dragon }},{Type.Grass, new[] { Type.Fire, Type.Grass, Type.Poison, Type.Flying, Type.Bug, Type.Dragon, Type.Steel }},
            {Type.Ice, new[] { Type.Fire, Type.Water, Type.Ice, Type.Steel }},{ Type.Fighting, new[] { Type.Poison, Type.Flying, Type.Psychic, Type.Bug, Type.Fairy }},
            {Type.Poison, new[] { Type.Poison, Type.Ground, Type.Rock, Type.Ghost }},{Type.Ground, new[] { Type.Grass, Type.Bug }},
            {Type.Fairy, new[] { Type.Poison, Type.Steel }},{Type.Steel, new[] { Type.Fire, Type.Fighting, Type.Ground }},
            {Type.Dark, new[] { Type.Fighting, Type.Bug, Type.Fairy }},{Type.Dragon, new[] { Type.Ice, Type.Dragon, Type.Fairy }},
            {Type.Ghost, new[] { Type.Ghost, Type.Dark }}, {Type.Rock, new[] { Type.Water, Type.Grass, Type.Fighting, Type.Ground, Type.Steel }},
            {Type.Bug, new[] { Type.Fire, Type.Flying, Type.Rock }}, {Type.Psychic, new[] { Type.Bug, Type.Ghost, Type.Dark }},
            {Type.Flying, new[] { Type.Electric, Type.Ice, Type.Rock }}
        };
        public static readonly IDictionary<Type, Type[]> NoEffect = new Dictionary<Type, Type[]>()
        {
            {Type.Normal, new[] { Type.Ghost }}, {Type.Electric, new[] { Type.Ground}},
            {Type.Fighting, new[] { Type.Ghost }}, {Type.Poison, new[] { Type.Steel }}, 
            {Type.Ground, new[] { Type.Flying }}, {Type.Psychic, new[] { Type.Dark }},
            {Type.Ghost, new[] { Type.Normal }}, {Type.Dragon, new[] { Type.Fairy }},
        };
    }
}
