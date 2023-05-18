﻿using BlazorCRUD.Client.Pages;
using BlazorCRUD.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCRUD.Client.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager navigationManager;

        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();

        public SuperHeroService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            this.navigationManager = navigationManager;
        }

        public async Task GetComics()
        {
            var result = await _http.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
            if (result != null)
                Comics = result;
        }

        public async Task<SuperHero> GetSingleHero(int? id)
        {
            var result = await _http.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
            if (result != null)
                return result;
            throw new Exception("Hero Not Found");
        }

        public async Task GetSuperHeroes()
        {
            var result = await _http.GetFromJsonAsync<List<SuperHero>>("api/superhero");
            if (result != null)
            {
                Heroes = result;
            }
        }

        public async Task CreateHero(SuperHero hero)
        {
            var result = await _http.PostAsJsonAsync("/api/superhero", hero);
            var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();

            if (response != null)
                Heroes = response;
            this.navigationManager.NavigateTo("superheroes");
        }

        public async Task UpdateHero(SuperHero hero)
        {
            var result = await _http.PutAsJsonAsync($"/api/superhero/{hero.Id}", hero);
            var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();

            if (response != null)
                Heroes = response;
            this.navigationManager.NavigateTo("superheroes");
        }

        public async Task DeleteHero(int id)
        {
            var result = await _http.DeleteAsync($"/api/superhero/{id}");
            var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();

            if (response != null)
                Heroes = response;
            this.navigationManager.NavigateTo("superheroes");
        }
    }
}
