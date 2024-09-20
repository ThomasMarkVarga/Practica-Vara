using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlazorDecontApp.Services
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public class WeatherService : IWeatherService
    {
        HttpClient httpClient;

        public WeatherService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<WeatherForecast[]> GetForecasts()
        {
            HttpResponseMessage request = await httpClient.GetAsync(new Uri("https://localhost:7299/WeatherForecast"));
            string stringJSON = await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherForecast[]>(stringJSON);
        }
    }
}
