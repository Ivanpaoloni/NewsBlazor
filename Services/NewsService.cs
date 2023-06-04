using Microsoft.AspNetCore.Http.HttpResults;
using NewsBlazor.Models;
using NewsBlazor.Pages;
using System.Net;

namespace NewsBlazor.Services
{
    public class NewsService
    {
        private readonly HttpClient httpClient;

        public NewsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<News> GetNewsById(string id)
        {

            var apiUrl = $"https://localhost:7081/api/News/{id}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var news = await httpClient.GetFromJsonAsync<News>(apiUrl);
                return news;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Error al obtener la noticia.");
            }

        }
    }
}
