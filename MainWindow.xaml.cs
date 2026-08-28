using MeteoNewsWidget.Models;
using MeteoNewsWidget.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MeteoNewsWidget
{
    public partial class MainWindow : Window
    {
        // --- Services ---
        private WeatherService _weatherService = null!;
        private NewsService _newsService = null!;
        private WikiService _wikiService = null!;

        // --- Configuration ---
        private AppConfig _config = null!;

        // --- État ---
        private NewsFilter _currentFilter = NewsFilter.National;
        private List<NewsItem> _currentNews = new();
        private List<WikiItem> _currentWiki = new();
        private int _newsIndex = 0;
        private int _wikiIndex = 0;
        private string _countryCode = "FR";
        private string _currentNewsLink = "";
        private string _currentWikiLink = "";
        private string _currentDailyLink = "";

        // --- Timers ---
        private DispatcherTimer _weatherTimer = null!;
        private DispatcherTimer _newsTimer = null!;
        private DispatcherTimer _wikiTimer = null!;
        private DispatcherTimer _rotateTimer = null!;

        // --- Icônes météo locales ---
        private readonly Dictionary<string, string> _iconMap = new()
        {
            { "01d", "clear-day"     }, { "01n", "clear-night"  },
            { "02d", "partly-cloudy" }, { "02n", "partly-cloudy"},
            { "03d", "cloudy"        }, { "03n", "cloudy"       },
            { "04d", "cloudy"        }, { "04n", "cloudy"       },
            { "09d", "rain"          }, { "09n", "rain"         },
            { "10d", "rain"          }, { "10n", "rain"         },
            { "11d", "storm"         }, { "11n", "storm"        },
            { "13d", "snow"          }, { "13n", "snow"         },
            { "50d", "mist"          }, { "50n", "mist"         },
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        // =====================================================
        // INITIALISATION
        // =====================================================

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _config = ConfigService.Load();
            ConfigService.CreateExample();

            // Charger la langue avant tout le reste
            LocalizationService.Load(_config.Language);
            ApplyLocalization();
            Topmost = _config.AlwaysOnTop;

            // Charger le logo Wikipedia depuis les ressources locales
            try
            {
                string logoPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Icons", "wikipedia.png");

                if (System.IO.File.Exists(logoPath))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(logoPath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ImgWikiLogo.Source = bitmap;
                }
            }
            catch { }

            // Initialiser les services
            _weatherService = new WeatherService(_config.OpenWeatherApiKey, _config.Units);
            _newsService = new NewsService();
            _wikiService = new WikiService(_config.WikiUsername, _config.WikiToken);

            await RefreshWeatherAsync();
            await RefreshNewsAsync();
            await RefreshWikiAsync();

            SetActiveTab(BtnNational);

            _weatherTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(30) };
            _weatherTimer.Tick += async (s, e) => await RefreshWeatherAsync();
            _weatherTimer.Start();

            _newsTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(10) };
            _newsTimer.Tick += async (s, e) => await RefreshNewsAsync();
            _newsTimer.Start();

            _wikiTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(5) };
            _wikiTimer.Tick += async (s, e) => await RefreshWikiAsync();
            _wikiTimer.Start();

            _rotateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            _rotateTimer.Tick += RotateTimer_Tick;
            _rotateTimer.Start();
        }

        /// <summary>
        /// Applique les chaînes traduites à tous les éléments de l'interface.
        /// </summary>
        private void ApplyLocalization()
        {
            BtnLocal.Content = LocalizationService.Get("TabLocal");
            BtnNational.Content = LocalizationService.Get("TabNational");
            BtnIntl.Content = LocalizationService.Get("TabIntl");
            BtnTheme.Content = LocalizationService.Get("TabTheme");
            BtnTheme.ToolTip = LocalizationService.Get("ThemeTooltip");
            TxtWikiLabel.Text = LocalizationService.Get("WikiLabel");
            TxtWatchlistLabel.Text = LocalizationService.Get("WikiWatchlistLabel");
            TxtNews.Text = LocalizationService.Get("NewsLoading");
            TxtWikiDaily.Text = LocalizationService.Get("WikiLoading");
            TxtWiki.Text = LocalizationService.Get("WikiLoading");
        }

        // =====================================================
        // MÉTÉO
        // =====================================================

        private async Task RefreshWeatherAsync()
        {
            var weather = await _weatherService.GetCurrentWeatherAsync(_config.City);
            if (weather == null) return;

            _countryCode = weather.CountryCode;
            TxtCity.Text = weather.CityName;
            string unit = LocalizationService.GetLangConfig().TempUnit;
            TxtTemp.Text = $"{Math.Round(weather.Temperature)}{unit}";
            TxtDesc.Text = weather.Description;

            if (_iconMap.TryGetValue(weather.IconCode, out string? iconName))
            {
                try
                {
                    string iconPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Icons", $"{iconName}.png");

                    if (System.IO.File.Exists(iconPath))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(iconPath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        ImgWeatherIcon.Source = bitmap;
                    }
                }
                catch { ImgWeatherIcon.Source = null; }
            }

            var forecast = await _weatherService.GetForecastAsync(_config.City);
            if (forecast != null)
            {
                string tomorrow = LocalizationService.Get("Tomorrow");
                string dayAfter = LocalizationService.Get("DayAfterTomorrow");
                string tempUnit = LocalizationService.GetLangConfig().TempUnit;
                TxtDay1.Text = $"{tomorrow} : {Math.Round(forecast.Day1Temp)}{tempUnit} – {forecast.Day1Description}";
                TxtDay2.Text = $"{dayAfter} : {Math.Round(forecast.Day2Temp)}{tempUnit} – {forecast.Day2Description}";
            }
        }

        // =====================================================
        // ACTUALITÉS
        // =====================================================

        private async Task RefreshNewsAsync()
        {
            _currentNews = _currentFilter switch
            {
                NewsFilter.Local => await _newsService.GetLocalNewsAsync(_config.City, _countryCode),
                NewsFilter.National => await _newsService.GetNationalNewsAsync(_countryCode),
                NewsFilter.International => await _newsService.GetInternationalNewsAsync(),
                NewsFilter.Theme => await _newsService.GetThemeNewsAsync(_config.NewsThemeKeyword),
                _ => new List<NewsItem>()
            };
            _newsIndex = 0;
            ShowCurrentNews();
        }

        private void ShowCurrentNews()
        {
            if (_currentNews.Count == 0)
            {
                TxtNews.Text = LocalizationService.Get("NewsLoading");
                TxtNews.ToolTip = null;
                _currentNewsLink = "";
                return;
            }
            var item = _currentNews[_newsIndex];
            TxtNews.Text = item.Title;
            TxtNews.ToolTip = item.Title;
            _currentNewsLink = item.Link;
        }

        // =====================================================
        // WIKIPEDIA
        // =====================================================

        private async Task RefreshWikiAsync()
        {
            var daily = await _wikiService.GetArticleDuJourAsync();
            if (daily != null)
            {
                TxtWikiDaily.Text = daily.Title;
                TxtWikiDaily.ToolTip = daily.Extract != "" ? daily.Extract : daily.Title;
                _currentDailyLink = daily.Link;
            }
            else
            {
                TxtWikiDaily.Text = LocalizationService.Get("WikiLoading");
                TxtWikiDaily.ToolTip = null;
                _currentDailyLink = "";
            }

            if (_wikiService.HasAccount)
            {
                SepWatchlist.Visibility = Visibility.Visible;
                TxtWatchlistLabel.Visibility = Visibility.Visible;
                TxtWiki.Visibility = Visibility.Visible;

                _currentWiki = await _wikiService.GetWatchlistAsync();
                _wikiIndex = 0;
                ShowCurrentWiki();
            }
            else
            {
                SepWatchlist.Visibility = Visibility.Collapsed;
                TxtWatchlistLabel.Visibility = Visibility.Collapsed;
                TxtWiki.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowCurrentWiki()
        {
            if (_currentWiki.Count == 0)
            {
                TxtWiki.Text = LocalizationService.Get("WikiLoading");
                TxtWiki.ToolTip = null;
                _currentWikiLink = "";
                return;
            }
            var item = _currentWiki[_wikiIndex];
            TxtWiki.Text = item.Title;
            TxtWiki.ToolTip = item.Title;
            _currentWikiLink = item.Link;
        }

        // =====================================================
        // ROTATION AUTOMATIQUE (toutes les 30 secondes)
        // =====================================================

        private void RotateTimer_Tick(object? sender, EventArgs e)
        {
            if (_currentNews.Count > 0)
            {
                _newsIndex = (_newsIndex + 1) % _currentNews.Count;
                ShowCurrentNews();
            }

            if (_currentWiki.Count > 0)
            {
                _wikiIndex = (_wikiIndex + 1) % _currentWiki.Count;
                ShowCurrentWiki();
            }
        }

        // =====================================================
        // ONGLETS ACTUALITÉS
        // =====================================================

        private void SetActiveTab(System.Windows.Controls.Button activeBtn)
        {
            var inactive = (SolidColorBrush)FindResource("TabInactiveBrush");
            var active = (SolidColorBrush)FindResource("TabActiveBrush");
            BtnLocal.Foreground = inactive;
            BtnNational.Foreground = inactive;
            BtnIntl.Foreground = inactive;
            BtnTheme.Foreground = inactive;
            activeBtn.Foreground = active;
        }

        private async void BtnLocal_Click(object sender, RoutedEventArgs e)
        {
            _currentFilter = NewsFilter.Local;
            SetActiveTab(BtnLocal);
            await RefreshNewsAsync();
        }

        private async void BtnNational_Click(object sender, RoutedEventArgs e)
        {
            _currentFilter = NewsFilter.National;
            SetActiveTab(BtnNational);
            await RefreshNewsAsync();
        }

        private async void BtnIntl_Click(object sender, RoutedEventArgs e)
        {
            _currentFilter = NewsFilter.International;
            SetActiveTab(BtnIntl);
            await RefreshNewsAsync();
        }

        private async void BtnTheme_Click(object sender, RoutedEventArgs e)
        {
            _currentFilter = NewsFilter.Theme;
            SetActiveTab(BtnTheme);
            await RefreshNewsAsync();
        }

        private void BtnTheme_RightClick(object sender, MouseButtonEventArgs e)
        {
            string? keyword = Microsoft.VisualBasic.Interaction.InputBox(
                LocalizationService.Get("ThemeInputPrompt"),
                LocalizationService.Get("ThemeInputTitle"),
                _config.NewsThemeKeyword);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                _config.NewsThemeKeyword = keyword;
                ConfigService.Save(_config);
                if (_currentFilter == NewsFilter.Theme)
                    _ = RefreshNewsAsync();
            }
        }

        // =====================================================
        // CLICS SUR LES ARTICLES
        // =====================================================

        private void TxtNews_Click(object sender, MouseButtonEventArgs e)
        {
            OpenUrl(_currentNewsLink);
        }

        private void TxtWikiDaily_Click(object sender, MouseButtonEventArgs e)
        {
            OpenUrl(_currentDailyLink);
        }

        private void TxtWiki_Click(object sender, MouseButtonEventArgs e)
        {
            OpenUrl(_currentWikiLink);
        }

        private static void OpenUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch { }
        }

        // =====================================================
        // PARAMÈTRES / FERMETURE / DÉPLACEMENT
        // =====================================================

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_config);
            settingsWindow.Owner = this;
            if (settingsWindow.ShowDialog() == true)
            {
                _config = ConfigService.Load();
                Topmost = _config.AlwaysOnTop;

                // Charger le logo Wikipedia depuis les ressources locales
                try
                {
                    string logoPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Icons", "wikipedia.png");

                    MessageBox.Show($"Chemin : {logoPath}\nExiste : {System.IO.File.Exists(logoPath)}");

                    if (System.IO.File.Exists(logoPath))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(logoPath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        ImgWikiLogo.Source = bitmap;
                    }
                }
                catch { }

                // Recharger la localisation si la langue a changé
                LocalizationService.Load(_config.Language);
                ApplyLocalization();

                _weatherService = new WeatherService(_config.OpenWeatherApiKey, _config.Units);
                _wikiService = new WikiService(_config.WikiUsername, _config.WikiToken);

                _ = RefreshWeatherAsync();
                _ = RefreshNewsAsync();
                _ = RefreshWikiAsync();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Documents.Run ||
                e.OriginalSource == TxtNews ||
                e.OriginalSource == TxtWikiDaily ||
                e.OriginalSource == TxtWiki)
                return;

            DragMove();
        }
    }
}