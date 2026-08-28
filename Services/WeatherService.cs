using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeteoNewsWidget.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _units;
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

        public WeatherService(string apiKey, string units = "metric")
        {
            _apiKey = apiKey;
            _units = units;
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData?> GetCurrentWeatherAsync(string city)
        {
            try
            {
                // Langue récupérée depuis LocalizationService
                var (_, _, _, _, owmLang, units, _) = LocalizationService.GetLangConfig();
                string url = $"{BaseUrl}/weather?q={city}&appid={_apiKey}&units={units}&lang={owmLang}";
                string json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                return new WeatherData
                {
                    CityName = root.GetProperty("name").GetString() ?? city,
                    CountryCode = root.GetProperty("sys").GetProperty("country").GetString() ?? "",
                    Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                    Description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                    IconCode = root.GetProperty("weather")[0].GetProperty("icon").GetString() ?? ""
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<ForecastData?> GetForecastAsync(string city)
        {
            try
            {
                var (_, _, _, _, owmLang, units, _) = LocalizationService.GetLangConfig();
                string url = $"{BaseUrl}/forecast?q={city}&appid={_apiKey}&units={units}&lang={owmLang}&cnt=17";
                string json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                var list = doc.RootElement.GetProperty("list");

                var day1 = list[7];
                var day2 = list[14];

                return new ForecastData
                {
                    Day1Temp = day1.GetProperty("main").GetProperty("temp").GetDouble(),
                    Day1Description = day1.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                    Day2Temp = day2.GetProperty("main").GetProperty("temp").GetDouble(),
                    Day2Description = day2.GetProperty("weather")[0].GetProperty("description").GetString() ?? ""
                };
            }
            catch
            {
                return null;
            }
        }
    }

    public class WeatherData
    {
        public string CityName { get; set; } = "";
        public string CountryCode { get; set; } = "";
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public string Description { get; set; } = "";
        public string IconCode { get; set; } = "";
    }

    public class ForecastData
    {
        public double Day1Temp { get; set; }
        public string Day1Description { get; set; } = "";
        public double Day2Temp { get; set; }
        public string Day2Description { get; set; } = "";
    }
}
