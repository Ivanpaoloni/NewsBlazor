using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherDataByCoordinatesAsync(double latitude, double longitude, string apiKey)
    {
        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
            return await _httpClient.GetFromJsonAsync<WeatherData>(url);
        }
        catch (Exception ex) 
        {
            return null;
        }
    }
}

public class WeatherData
{
    public string Name { get; set; }
    public MainData Main { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
}
