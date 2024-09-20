namespace BlazorDecontApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast[]> GetForecasts();
    }
}
