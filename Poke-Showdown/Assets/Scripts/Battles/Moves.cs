using System;
using System.Collections;
using System.Collections.Generic;
using MoveData;
using Newtonsoft.Json;
using PokemonData;
using PokeStatus;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Battles
{
    public class Moves:MonoBehaviour
    {

        private readonly List<MonMove> _movePoolAttack = new();
        public readonly List<MonMove> moveSet = new();
        private int _randomNum;

        private int _moveCounter;

        private bool _poolFull;

        private IEnumerator GetRequest(string uri)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            yield return webRequest.SendWebRequest();
        
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError: //connection error or dataprocessing error, log an error in console
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    var moveStat = JsonConvert.DeserializeObject<MoveStats>(webRequest.downloadHandler.text);
                        
                    int? movePower = moveStat.power;
                    _poolFull = false;
                    
                    Enum.TryParse(moveStat.type.name,true,out PokemonData.Type type);
        
                    if (movePower.HasValue)
                    {
                        _movePoolAttack.Add(new MonMove
                        {
                            currentPp = moveStat.pp.Value,
                            pp = moveStat.pp.Value,
                            moveName = moveStat.name,
                            power = movePower.Value,
                            priority = moveStat.priority.Value,
                            type = type,
                            accuracy = moveStat.accuracy ?? 0,
                        });
                    }
                    _poolFull = true;
                    break;
            }
        }

    // Update is called once per frame
        private void Update()
        {
            if (_poolFull && _moveCounter <= 3)
            {
                _randomNum = Random.Range(0, _movePoolAttack.Count - 1); //number in between 0 and end of movePoolAttack list
                moveSet.Add(_movePoolAttack[_randomNum]);
                _moveCounter++;
            }
            
        }

        public void Initialize(List<Move> moves)
        {
            foreach (var move in moves)
            {
                StartCoroutine(GetRequest("https://pokeapi.co/api/v2/move/" + move.move.name));
            }
        }
    }

    public class Ailment
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ContestCombos
    {
        public Normal normal { get; set; }
        public Super super { get; set; }
    }

    public class ContestEffect
    {
        public string url { get; set; }
    }

    public class ContestType
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class DamageClass
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EffectEntry
    {
        public string effect { get; set; }
        public Language language { get; set; }
        public string short_effect { get; set; }
    }

    public class FlavorTextEntry
    {
        public string flavor_text { get; set; }
        public Language language { get; set; }
        public VersionGroup version_group { get; set; }
    }

    public class Generation
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class LearnedByPokemon
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Machine
    {
        public Machine machine { get; set; }
        public VersionGroup version_group { get; set; }
    }

    public class Machine2
    {
        public string url { get; set; }
    }

    public class Meta
    {
        public Ailment ailment { get; set; }
        public int? ailment_chance { get; set; }
        public Category category { get; set; }
        public int? crit_rate { get; set; }
        public int? drain { get; set; }
        public int? flinch_chance { get; set; }
        public int? healing { get; set; }
        public object max_hits { get; set; }
        public object max_turns { get; set; }
        public object min_hits { get; set; }
        public object min_turns { get; set; }
        public int? stat_chance { get; set; }
    }

    public class Name
    {
        public Language language { get; set; }
        public string name { get; set; }
    }

    public class Normal
    {
        public List<UseAfter> use_after { get; set; }
        public object use_before { get; set; }
    }

    public class MoveStats
    {
        public int? accuracy { get; set; }
        public ContestCombos contest_combos { get; set; }
        public ContestEffect contest_effect { get; set; }
        public ContestType contest_type { get; set; }
        public DamageClass damage_class { get; set; }
        public object effect_chance { get; set; }
        public List<object> effect_changes { get; set; }
        public List<EffectEntry> effect_entries { get; set; }
        public List<FlavorTextEntry> flavor_text_entries { get; set; }
        public Generation generation { get; set; }
        public int? id { get; set; }
        public List<LearnedByPokemon> learned_by_pokemon { get; set; }
        public List<Machine> machines { get; set; }
        public Meta meta { get; set; }
        public string name { get; set; }
        public List<Name> names { get; set; }
        public List<object> past_values { get; set; }
        public int? power { get; set; }
        public int? pp { get; set; }
        public int? priority { get; set; }
        public List<object> stat_changes { get; set; }
        public SuperContestEffect super_contest_effect { get; set; }
        public Target target { get; set; }
        public Type type { get; set; }
    }

    public class Super
    {
        public object use_after { get; set; }
        public object use_before { get; set; }
    }

    public class SuperContestEffect
    {
        public string url { get; set; }
    }

    public class Target
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class UseAfter
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class VersionGroup
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}