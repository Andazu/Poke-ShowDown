using System;
using System.Linq;
using Newtonsoft.Json;
using PokemonData;
using PokeStatus;

namespace PokeApiIntegration
{
    public static class ResponseParser
    {
        public static Pokemon ParsePokemon(string jsonResponse)
        {
            //API calls
            var name = JsonConvert.DeserializeObject<Form>(jsonResponse);
            var pokestats = JsonConvert.DeserializeObject<PokeStats>(jsonResponse);

            var stats = pokestats.stats.
                Select(s => Pokemon.CalculateOtherStat(s.base_stat,50)).ToArray();
            stats[0] = Pokemon.CalculateHp(stats[0],50);

            var types = pokestats.types
                    .Select(t => Enum.TryParse(t.type.name, true,out PokemonData.Type tEnum) ? tEnum : PokemonData.Type.None )
                    .ToArray();

            return new Pokemon
            {
                level = 50,
                stats = stats,
                currentHp = stats[0],
                type1 = types[0],
                type2 = types[1]
            };
        }
        
        
    }
}