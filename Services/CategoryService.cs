using Microsoft.AspNetCore.Mvc;
using NewsBlazor.Models;
using System.Net;
using System.Text.Json;

namespace NewsBlazor.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> Get();
        Task<Category> GetByName(string name);
    }
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Category>> Get()
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
                throw new Exception("Error al obtener la categoria.");
            }
        }

        public async Task<Category> GetByName(string name)
        {
            var categorySelected = new Category() { };
            var allCategories = await Get();
            categorySelected = allCategories.FirstOrDefault(c => c.name == name);
            if (categorySelected.name == name)            
            {
                return categorySelected;
            }
            else
            {
                throw new Exception("Error al obtener la categoria.");
            }

        }
    }
}
