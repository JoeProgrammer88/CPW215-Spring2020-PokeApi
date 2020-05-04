using PokeApiCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeApiWebsite.Models
{
    public static class PokeAPIHelper
    {
        /// <summary>
        /// Get a pokemon by id, moves will be sorted in
        /// alphabetical order
        /// </summary>
        /// <param name="desiredId"></param>
        /// <returns></returns>
        public static async Task<Pokemon> GetById(int desiredId)
        {
            PokeApiClient myClient = new PokeApiClient();
            Pokemon result = await myClient.GetPokemonById(desiredId);

            // Sort moves by name alphabetically
            result.moves.OrderBy(m => m.move.name);

            return result;
        }

        public static PokedexEntryViewModel GetPokedexEntryFromPokemon(Pokemon result)
        {
            var entry = new PokedexEntryViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Height = result.Height.ToString(),
                Weight = result.Weight.ToString(),
                PokedexImageUrl = result.Sprites.FrontDefault,
                MoveList = result.moves
                                .OrderBy(m => m.move.name)
                                .Select(m => m.move.name)
                                .ToArray()
                //MoveList = (from m in result.moves
                //            orderby m.move.name ascending
                //          select m.move.name).ToArray()
            };
            entry.Name = entry.Name.FirstCharToUpper();
            return entry;
        }

    }
}
