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
            var response = await _httpClient.GetFromJsonAsync<WeatherData>(url);
            if (response != null)
            {
                var weatherData = new WeatherData
                {
                    Name = response.Name,
                    Main = new MainData
                    {
                        Temp = (float)Math.Round(response.Main.Temp, 1), // Convierte a int para quitar los decimales
                        Humidity = response.Main.Humidity
                    },
                    Humidity = response.Humidity
                };

                return weatherData;
            }
            else
            {
                return null;
            }
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
    public float Humidity { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
    public float Humidity { get; set; } 
}
