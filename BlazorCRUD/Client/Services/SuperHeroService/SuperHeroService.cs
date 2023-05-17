﻿using BlazorCRUD.Shared;
using System.Net.Http.Json;

namespace BlazorCRUD.Client.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly HttpClient _http;

        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();

        public SuperHeroService(HttpClient http)
        {
            _http = http;
        }

        public Task GetComics()
        {
            throw new NotImplementedException();
        }

        public async Task<SuperHero> GetSingleHero(int id)
        {
            var result = await _http.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
            if (result != null)
                return result;
            throw new Exception("Hero Not Found");
        }

        public async Task GetSuperHeroes()
        {
            var result = await _http.GetFromJsonAsync<List<SuperHero>>("api/superhero");
            if(result != null)
            {
                Heroes = result;
            }
        }
    }
}
