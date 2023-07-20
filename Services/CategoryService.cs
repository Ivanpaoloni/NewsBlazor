using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NewsBlazor.Models;
using NewsBlazor.Pages;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace NewsBlazor.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> Get();
        Task<Category> GetById(int id);
        Task<Category> GetByName(string name);
        Task Create(Category category);
    }
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Create(Category category)
        {
            try
            {
                var apiUrl = "https://localhost:7081/api/category";

                var response = await httpClient.PostAsJsonAsync(apiUrl, category);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex) 
            {
            }
        }

        public async Task<List<Category>> Get()
        {
            try
            {
                var apiUrl = "https://localhost:7081/api/category";
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var allCategories = JsonSerializer.Deserialize<List<Category>>(jsonString);
                    return allCategories;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    return new List<Category>();
                    //throw new Exception("Error al obtener la categoria.");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                var apiUrl = $"https://localhost:7081/api/category/{id}";
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var category = await httpClient.GetFromJsonAsync<Category>(apiUrl);
                    return category;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception("Error al obtener la categoria.");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Category> GetByName(string name)
        {
            try
            {
                var apiUrl = $"https://localhost:7081/api/category/name/{name}";
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var category = await httpClient.GetFromJsonAsync<Category>(apiUrl);
                    return category;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception("Error al obtener la categoria.");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
