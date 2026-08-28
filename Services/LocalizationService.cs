using System.Collections.Generic;

namespace MeteoNewsWidget.Services
{
    public static class LocalizationService
    {
        private static Dictionary<string, string> _strings = new();
        private static string _currentLang = "fr";

        // Correspondance langue → (hl, gl, ceid, wikiLang, owmLang, units, tempUnit)
        public static readonly Dictionary<string, (string Hl, string Gl, string Ceid, string WikiLang, string OWMLang, string Units, string TempUnit)> LangConfig = new()
        {
            { "fr", ("fr", "FR", "FR:fr", "fr", "fr", "metric",   "°C") },
            { "en", ("en", "US", "US:en", "en", "en", "imperial", "°F") },
            { "es", ("es", "ES", "ES:es", "es", "es", "metric",   "°C") },
            { "de", ("de", "DE", "DE:de", "de", "de", "metric",   "°C") },
            { "it", ("it", "IT", "IT:it", "it", "it", "metric",   "°C") },
            { "pt", ("pt", "PT", "PT:pt", "pt", "pt", "metric",   "°C") },
        };

        private static readonly Dictionary<string, Dictionary<string, string>> _allStrings = new()
        {
            ["fr"] = new()
            {
                ["City"] = "Ville",
                ["ApiKey"] = "Clé API OpenWeatherMap",
                ["ThemeKeyword"] = "Mot-clé filtre Thème",
                ["WikiUsername"] = "Nom d'utilisateur Wikipedia",
                ["WikiToken"] = "Token Wikipedia (liste de suivi)",
                ["Language"] = "Langue",
                ["Save"] = "Enregistrer",
                ["Cancel"] = "Annuler",
                ["Settings"] = "Paramètres",
                ["TabLocal"] = "LOCAL",
                ["TabNational"] = "NATIONAL",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "THÈME",
                ["ThemeTooltip"] = "Clic droit pour changer le mot-clé",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Dernières modifications des pages surveillées",
                ["WikiDaily"] = "Article du jour",
                ["NewsLoading"] = "Chargement des actualités...",
                ["WikiLoading"] = "Chargement Wikipedia...",
                ["Tomorrow"] = "Demain",
                ["DayAfterTomorrow"] = "Après-demain",
                ["ThemeInputPrompt"] = "Entrez le mot-clé du filtre Thème :",
                ["ThemeInputTitle"] = "Changer le thème",
                ["AlwaysOnTop"] = "Toujours au premier plan",
            },
            ["en"] = new()
            {
                ["City"] = "City",
                ["ApiKey"] = "OpenWeatherMap API Key",
                ["ThemeKeyword"] = "Theme filter keyword",
                ["WikiUsername"] = "Wikipedia username",
                ["WikiToken"] = "Wikipedia token (watchlist)",
                ["Language"] = "Language",
                ["Save"] = "Save",
                ["Cancel"] = "Cancel",
                ["Settings"] = "Settings",
                ["TabLocal"] = "LOCAL",
                ["TabNational"] = "NATIONAL",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "THEME",
                ["ThemeTooltip"] = "Right-click to change keyword",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Latest changes on watched pages",
                ["WikiDaily"] = "Article of the day",
                ["NewsLoading"] = "Loading news...",
                ["WikiLoading"] = "Loading Wikipedia...",
                ["Tomorrow"] = "Tomorrow",
                ["DayAfterTomorrow"] = "Day after tomorrow",
                ["ThemeInputPrompt"] = "Enter the theme filter keyword:",
                ["ThemeInputTitle"] = "Change theme",
                ["AlwaysOnTop"] = "Always on top",
            },
            ["es"] = new()
            {
                ["City"] = "Ciudad",
                ["ApiKey"] = "Clave API OpenWeatherMap",
                ["ThemeKeyword"] = "Palabra clave del filtro Tema",
                ["WikiUsername"] = "Nombre de usuario Wikipedia",
                ["WikiToken"] = "Token Wikipedia (lista de seguimiento)",
                ["Language"] = "Idioma",
                ["Save"] = "Guardar",
                ["Cancel"] = "Cancelar",
                ["Settings"] = "Configuración",
                ["TabLocal"] = "LOCAL",
                ["TabNational"] = "NACIONAL",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "TEMA",
                ["ThemeTooltip"] = "Clic derecho para cambiar la palabra clave",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Últimas modificaciones de páginas vigiladas",
                ["WikiDaily"] = "Artículo del día",
                ["NewsLoading"] = "Cargando noticias...",
                ["WikiLoading"] = "Cargando Wikipedia...",
                ["Tomorrow"] = "Mañana",
                ["DayAfterTomorrow"] = "Pasado mañana",
                ["ThemeInputPrompt"] = "Ingrese la palabra clave del filtro Tema:",
                ["ThemeInputTitle"] = "Cambiar tema",
                ["AlwaysOnTop"] = "Siempre encima",
            },
            ["de"] = new()
            {
                ["City"] = "Stadt",
                ["ApiKey"] = "OpenWeatherMap API-Schlüssel",
                ["ThemeKeyword"] = "Thema-Filter Schlüsselwort",
                ["WikiUsername"] = "Wikipedia-Benutzername",
                ["WikiToken"] = "Wikipedia-Token (Beobachtungsliste)",
                ["Language"] = "Sprache",
                ["Save"] = "Speichern",
                ["Cancel"] = "Abbrechen",
                ["Settings"] = "Einstellungen",
                ["TabLocal"] = "LOKAL",
                ["TabNational"] = "NATIONAL",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "THEMA",
                ["ThemeTooltip"] = "Rechtsklick zum Ändern des Schlüsselworts",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Letzte Änderungen an beobachteten Seiten",
                ["WikiDaily"] = "Artikel des Tages",
                ["NewsLoading"] = "Nachrichten werden geladen...",
                ["WikiLoading"] = "Wikipedia wird geladen...",
                ["Tomorrow"] = "Morgen",
                ["DayAfterTomorrow"] = "Übermorgen",
                ["ThemeInputPrompt"] = "Thema-Filter Schlüsselwort eingeben:",
                ["ThemeInputTitle"] = "Thema ändern",
                ["AlwaysOnTop"] = "Immer im Vordergrund",
            },
            ["it"] = new()
            {
                ["City"] = "Città",
                ["ApiKey"] = "Chiave API OpenWeatherMap",
                ["ThemeKeyword"] = "Parola chiave filtro Tema",
                ["WikiUsername"] = "Nome utente Wikipedia",
                ["WikiToken"] = "Token Wikipedia (lista di controllo)",
                ["Language"] = "Lingua",
                ["Save"] = "Salva",
                ["Cancel"] = "Annulla",
                ["Settings"] = "Impostazioni",
                ["TabLocal"] = "LOCALE",
                ["TabNational"] = "NAZIONALE",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "TEMA",
                ["ThemeTooltip"] = "Clic destro per cambiare la parola chiave",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Ultime modifiche alle pagine monitorate",
                ["WikiDaily"] = "Articolo del giorno",
                ["NewsLoading"] = "Caricamento notizie...",
                ["WikiLoading"] = "Caricamento Wikipedia...",
                ["Tomorrow"] = "Domani",
                ["DayAfterTomorrow"] = "Dopodomani",
                ["ThemeInputPrompt"] = "Inserire la parola chiave del filtro Tema:",
                ["ThemeInputTitle"] = "Cambia tema",
                ["AlwaysOnTop"] = "Sempre in primo piano",
            },
            ["pt"] = new()
            {
                ["City"] = "Cidade",
                ["ApiKey"] = "Chave API OpenWeatherMap",
                ["ThemeKeyword"] = "Palavra-chave do filtro Tema",
                ["WikiUsername"] = "Nome de utilizador Wikipedia",
                ["WikiToken"] = "Token Wikipedia (lista de acompanhamento)",
                ["Language"] = "Idioma",
                ["Save"] = "Guardar",
                ["Cancel"] = "Cancelar",
                ["Settings"] = "Configurações",
                ["TabLocal"] = "LOCAL",
                ["TabNational"] = "NACIONAL",
                ["TabIntl"] = "INTL",
                ["TabTheme"] = "TEMA",
                ["ThemeTooltip"] = "Clique direito para alterar a palavra-chave",
                ["WikiLabel"] = "WIKIPEDIA",
                ["WikiWatchlistLabel"] = "Últimas modificações nas páginas vigiadas",
                ["WikiDaily"] = "Artigo do dia",
                ["NewsLoading"] = "A carregar notícias...",
                ["WikiLoading"] = "A carregar Wikipedia...",
                ["Tomorrow"] = "Amanhã",
                ["DayAfterTomorrow"] = "Depois de amanhã",
                ["ThemeInputPrompt"] = "Introduza a palavra-chave do filtro Tema:",
                ["ThemeInputTitle"] = "Alterar tema",
                ["AlwaysOnTop"] = "Sempre à frente",
            },
        };

        public static void Load(string lang)
        {
            _currentLang = LangConfig.ContainsKey(lang) ? lang : "fr";
            _strings = _allStrings.TryGetValue(_currentLang, out var s) ? s : _allStrings["fr"];
        }

        public static string Get(string key)
        {
            return _strings.TryGetValue(key, out string? val) ? val : key;
        }

        public static string CurrentLang => _currentLang;

        public static (string Hl, string Gl, string Ceid, string WikiLang, string OWMLang, string Units, string TempUnit) GetLangConfig()
        {
            return LangConfig.TryGetValue(_currentLang, out var cfg) ? cfg : LangConfig["fr"];
        }
    }
}
