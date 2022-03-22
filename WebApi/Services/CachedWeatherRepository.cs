using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Services
{
    public class CachedWeatherRepository : IWeatherRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IWeatherRepository _weatherRepository;

        private readonly string _getCacheKey = "GetWeather";

        public CachedWeatherRepository(IMemoryCache memoryCache, IWeatherRepository weatherRepository)
        {
            _memoryCache = memoryCache;
            _weatherRepository = weatherRepository;
        }

        public IEnumerable<WeatherForecast> GetWeather()
        {
            return _memoryCache.GetOrCreate(_getCacheKey, s => { 
                return _weatherRepository.GetWeather();
            });
        }

        public void UpdateWeather()
        {
            _weatherRepository.UpdateWeather();
            _memoryCache.Remove(_getCacheKey);
        }
    }
}
