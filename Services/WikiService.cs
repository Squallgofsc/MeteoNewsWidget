using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace MeteoNewsWidget.Services
{
    public class WikiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _username;
        private readonly string _token;

        public WikiService(string username, string token)
        {
            _username = username;
            _token = token;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "MeteoNewsWidget/1.0 (https://github.com)");
        }

        public bool HasAccount =>
            !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_token);

        public async Task<WikiItem?> GetArticleDuJourAsync()
        {
            var (_, _, _, wikiLang, _, _, _) = LocalizationService.GetLangConfig();

            if (wikiLang != "en")
            {
                var localItem = await TryGetFeaturedAsync(wikiLang);
                if (localItem != null) return localItem;

                var randomItem = await TryGetRandomAsync(wikiLang);
                if (randomItem != null) return randomItem;
            }

            return await TryGetFeaturedAsync("en");
        }

        private async Task<WikiItem?> TryGetFeaturedAsync(string lang)
        {
            try
            {
                string url = $"https://{lang}.wikipedia.org/api/rest_v1/feed/featured/{System.DateTime.Now:yyyy/MM/dd}";
                string json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("tfa", out var tfa))
                {
                    string title = tfa.GetProperty("titles").GetProperty("normalized").GetString() ?? "";
                    string extract = tfa.TryGetProperty("extract", out var ext) ? ext.GetString() ?? "" : "";
                    string link = tfa.GetProperty("content_urls").GetProperty("desktop").GetProperty("page").GetString() ?? "";

                    return new WikiItem
                    {
                        Title = $"{LocalizationService.Get("WikiDaily")} : {title}",
                        Link = link,
                        Extract = extract
                    };
                }
            }
            catch { }
            return null;
        }

        private async Task<WikiItem?> TryGetRandomAsync(string lang)
        {
            try
            {
                // Utilise l'API MediaWiki classique pour obtenir un article
                // aléatoire — fonctionne pour toutes les langues
                string url = $"https://{lang}.wikipedia.org/w/api.php" +
                    $"?action=query&list=random&rnnamespace=0&rnlimit=1&format=json";
                string json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                var pages = doc.RootElement
                               .GetProperty("query")
                               .GetProperty("random");

                if (pages.GetArrayLength() > 0)
                {
                    string title = pages[0].GetProperty("title").GetString() ?? "";
                    string encTitle = Uri.EscapeDataString(title);
                    string link = $"https://{lang}.wikipedia.org/wiki/{encTitle}";

                    return new WikiItem
                    {
                        Title = $"{LocalizationService.Get("WikiDaily")} : {title}",
                        Link = link,
                        Extract = ""
                    };
                }
            }
            catch { }
            return null;
        }

        public async Task<List<WikiItem>> GetWatchlistAsync()
        {
            var items = new List<WikiItem>();
            if (!HasAccount) return items;

            try
            {
                var (_, _, _, wikiLang, _, _, _) = LocalizationService.GetLangConfig();
                string url = $"https://{wikiLang}.wikipedia.org/w/api.php" +
                    $"?action=feedwatchlist&allrev&wlowner={_username}" +
                    $"&wltoken={_token}&feedformat=rss";
                string xml = await _httpClient.GetStringAsync(url);

                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var nodes = doc.SelectNodes("//channel/item");
                if (nodes == null) return items;

                int count = 0;
                foreach (XmlNode node in nodes)
                {
                    if (count >= 3) break;
                    string title = node.SelectSingleNode("title")?.InnerText ?? "";
                    string link = node.SelectSingleNode("link")?.InnerText ?? "";
                    items.Add(new WikiItem { Title = title, Link = link });
                    count++;
                }
            }
            catch { }

            return items;
        }
    }

    public class WikiItem
    {
        public string Title { get; set; } = "";
        public string Link { get; set; } = "";
        public string Extract { get; set; } = "";
    }
}