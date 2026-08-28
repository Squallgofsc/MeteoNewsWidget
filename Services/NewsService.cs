using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace MeteoNewsWidget.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public Task<List<NewsItem>> GetLocalNewsAsync(string city, string countryCode)
        {
            var (hl, _, _, _, _, _, _) = LocalizationService.GetLangConfig();
            string url = $"https://news.google.com/rss/search?q={Uri.EscapeDataString(city)}&hl={hl}&gl={countryCode}&ceid={countryCode}:{hl}";
            return FetchRssAsync(url);
        }

        public Task<List<NewsItem>> GetNationalNewsAsync(string countryCode)
        {
            var (hl, _, _, _, _, _, _) = LocalizationService.GetLangConfig();
            string url = $"https://news.google.com/rss?hl={hl}&gl={countryCode}&ceid={countryCode}:{hl}";
            return FetchRssAsync(url);
        }

        public Task<List<NewsItem>> GetInternationalNewsAsync()
        {
            var (hl, gl, ceid, _, _, _, _) = LocalizationService.GetLangConfig();
            string url = $"https://news.google.com/rss/headlines/section/topic/WORLD?hl={hl}&gl={gl}&ceid={ceid}";
            return FetchRssAsync(url);
        }

        public Task<List<NewsItem>> GetThemeNewsAsync(string keyword)
        {
            var (hl, gl, ceid, _, _, _, _) = LocalizationService.GetLangConfig();
            string url = $"https://news.google.com/rss/search?q={Uri.EscapeDataString(keyword)}&hl={hl}&gl={gl}&ceid={ceid}";
            return FetchRssAsync(url);
        }

        private async Task<List<NewsItem>> FetchRssAsync(string url)
        {
            var items = new List<NewsItem>();
            try
            {
                string xml = await _httpClient.GetStringAsync(url);
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var nodes = doc.SelectNodes("//channel/item");
                if (nodes == null) return items;

                int count = 0;
                foreach (XmlNode node in nodes)
                {
                    if (count >= 5) break;
                    string title = node.SelectSingleNode("title")?.InnerText ?? "";
                    string link = node.SelectSingleNode("link")?.InnerText ?? "";
                    items.Add(new NewsItem { Title = title, Link = link });
                    count++;
                }
            }
            catch { }

            return items;
        }
    }

    public class NewsItem
    {
        public string Title { get; set; } = "";
        public string Link { get; set; } = "";
    }

    public enum NewsFilter
    {
        Local,
        National,
        International,
        Theme
    }
}
