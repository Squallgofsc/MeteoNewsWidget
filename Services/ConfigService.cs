using MeteoNewsWidget.Models;
using System.IO;
using System.Text.Json;

namespace MeteoNewsWidget.Services
{
    /// <summary>
    /// Gère la lecture et l'écriture de config.json.
    /// config.json est exclu du dépôt GitHub via .gitignore.
    /// config.example.json (vierge) est inclus pour les nouveaux utilisateurs.
    /// </summary>
    public class ConfigService
    {
        private static readonly string ConfigPath = "config.json";
        private static readonly string ExamplePath = "config.example.json";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        /// <summary>
        /// Charge la configuration depuis config.json.
        /// Si le fichier n'existe pas, crée une configuration par défaut.
        /// </summary>
        public static AppConfig Load()
        {
            if (!File.Exists(ConfigPath))
            {
                var defaultConfig = new AppConfig();
                Save(defaultConfig);
                return defaultConfig;
            }

            try
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            catch
            {
                return new AppConfig();
            }
        }

        /// <summary>
        /// Sauvegarde la configuration dans config.json.
        /// </summary>
        public static void Save(AppConfig config)
        {
            string json = JsonSerializer.Serialize(config, JsonOptions);
            File.WriteAllText(ConfigPath, json);
        }

        /// <summary>
        /// Crée config.example.json avec des valeurs vierges.
        /// Ce fichier est inclus dans le dépôt GitHub pour guider les nouveaux utilisateurs.
        /// </summary>
        public static void CreateExample()
        {
            var example = new AppConfig
            {
                City = "VotreVille",
                OpenWeatherApiKey = "VOTRE_CLE_API_OPENWEATHERMAP",
                Units = "metric",
                NewsThemeKeyword = "technologie",
                WikiUsername = "VOTRE_USERNAME_WIKIPEDIA",
                WikiToken = "VOTRE_TOKEN_WIKIPEDIA"
            };

            string json = JsonSerializer.Serialize(example, JsonOptions);
            File.WriteAllText(ExamplePath, json);
        }
    }
}