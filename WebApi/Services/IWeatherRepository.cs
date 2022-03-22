namespace WebApi.Services
{
    public interface IWeatherRepository
    {
        IEnumerable<WeatherForecast> GetWeather();
        void UpdateWeather();
    }
}
