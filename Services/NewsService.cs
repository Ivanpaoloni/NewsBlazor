using Microsoft.AspNetCore.Http.HttpResults;
using NewsBlazor.Models;
using NewsBlazor.Pages;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace NewsBlazor.Services
{
    public interface INewsService
    {
        Task<List<News>> GetNewsByCategory(int category);
        Task<News> GetNewsById(string id);
    }
    public class NewsService : INewsService
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
        public async Task<List<News>> GetNewsByCategory(int category)
        {

            List<News> newsCategorized;
            var apiUrl = $"https://localhost:7081/api/News/sections/{category}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var allNews = JsonSerializer.Deserialize<List<News>>(jsonString);
                newsCategorized = allNews.OrderByDescending(n => n.publicationDate).Take(6).ToList();
                return newsCategorized;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Error al obtener las noticias.");
            }

            //List<News> newsCategorized;
            //var apiUrl = $"https://localhost:7081/api/News/";
            //var response = await httpClient.GetAsync(apiUrl);

            //if (response.IsSuccessStatusCode)
            //{
            //    var list = new List<News>();
            //    var jsonString = await response.Content.ReadAsStringAsync();
            //    var allNews = JsonSerializer.Deserialize<List<News>>(jsonString);
            //    foreach (var news in allNews)
            //    {
            //        if (news.categoryId == id)
            //        {
            //            list.Add(news);
            //        }
            //    }
            //    newsCategorized = list.OrderByDescending(n => n.publicationDate).Take(6).ToList();

            //    return newsCategorized;
            //}
            //else if (response.StatusCode == HttpStatusCode.NotFound)
            //{
            //    return null;
            //}
            //else
            //{
            //    throw new Exception("Error al obtener la noticia.");
            //}
        }
    }
}
