using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokemonWebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PokemonWebApp.Pages.Pokemons
{
    public class DetailModel : PageModel
    {
        public Pokemon Pokemon { get; set; }
        string baseUrl = "https://localhost:44321/";
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);
                }
            }

            return Page();
        }
    }
}
