namespace MeteoNewsWidget.Models
{
    /// <summary>
    /// Structure de configuration de l'application.
    /// Chargée depuis config.json (exclu du dépôt GitHub via .gitignore).
    /// </summary>
    public class AppConfig
    {
        // --- Météo ---
        public string City { get; set; } = "Paris";
        public string OpenWeatherApiKey { get; set; } = "";
        public string Units { get; set; } = "metric";

        // --- Langue (code ISO 639-1 : fr, en, es, de, it, pt) ---
        public string Language { get; set; } = "fr";

        // --- Actualités ---
        public string NewsThemeKeyword { get; set; } = "technologie";

        // --- Wikipedia ---
        public string WikiUsername { get; set; } = "";
        public string WikiToken { get; set; } = "";

        // --- Comportement fenêtre ---
        public bool AlwaysOnTop { get; set; } = true;
    }
}