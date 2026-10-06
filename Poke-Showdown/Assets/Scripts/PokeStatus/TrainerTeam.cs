using PokemonData;

namespace PokeStatus
{
    public class TrainerTeam
    {
        public Pokemon[]  pokemon;
        public Pokemon activePokemon;
        public int numLivePokemon;

        public void SwitchPokemon(int pokemonIndex)
        {
            pokemon[0] = pokemon[pokemonIndex];
            pokemon[pokemonIndex] = activePokemon;
            activePokemon = pokemon[0];
        }

        public void InitializeTeam(int numPokemon)
        {
            pokemon = new Pokemon[numPokemon];
            
        }

    }
}