using MeteoNewsWidget.Models;
using MeteoNewsWidget.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MeteoNewsWidget
{
    public partial class SettingsWindow : Window
    {
        private readonly AppConfig _config;

        private readonly List<(string Code, string Name)> _languages = new()
        {
            ("fr", "Français"),
            ("en", "English"),
            ("es", "Español"),
            ("de", "Deutsch"),
            ("it", "Italiano"),
            ("pt", "Português")
        };

        public SettingsWindow(AppConfig config)
        {
            InitializeComponent();
            _config = config;

            // Appliquer la localisation
            ApplyLocalization();

            // Remplir la ListBox des langues
            foreach (var (code, name) in _languages)
            {
                CmbLanguage.Items.Add(new ListBoxItem
                {
                    Content = name,
                    Tag = code
                });
            }

            // Sélectionner la langue actuelle
            foreach (ListBoxItem item in CmbLanguage.Items)
            {
                if (item.Tag?.ToString() == config.Language)
                {
                    CmbLanguage.SelectedItem = item;
                    break;
                }
            }

            // Pré-remplir les champs
            TxtCity.Text = config.City;
            TxtApiKey.Text = config.OpenWeatherApiKey;
            TxtTheme.Text = config.NewsThemeKeyword;
            TxtWikiUser.Text = config.WikiUsername;
            TxtWikiToken.Text = config.WikiToken;
            ChkAlwaysOnTop.IsChecked = config.AlwaysOnTop;
        }

        private void ApplyLocalization()
        {
            TxtTitle.Text = LocalizationService.Get("Settings");
            LblLanguage.Text = LocalizationService.Get("Language");
            LblCity.Text = LocalizationService.Get("City");
            LblApiKey.Text = LocalizationService.Get("ApiKey");
            LblTheme.Text = LocalizationService.Get("ThemeKeyword");
            LblWikiUser.Text = LocalizationService.Get("WikiUsername");
            LblWikiToken.Text = LocalizationService.Get("WikiToken");
            BtnSave.Content = LocalizationService.Get("Save");
            BtnCancel.Content = LocalizationService.Get("Cancel");
            ChkAlwaysOnTop.Content = LocalizationService.Get("AlwaysOnTop");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer la langue sélectionnée
            if (CmbLanguage.SelectedItem is ListBoxItem selected)
                _config.Language = selected.Tag?.ToString() ?? "fr";

            _config.City = TxtCity.Text.Trim();
            _config.OpenWeatherApiKey = TxtApiKey.Text.Trim();
            _config.NewsThemeKeyword = TxtTheme.Text.Trim();
            _config.WikiUsername = TxtWikiUser.Text.Trim();
            _config.WikiToken = TxtWikiToken.Text.Trim();
            _config.AlwaysOnTop = ChkAlwaysOnTop.IsChecked == true;

            ConfigService.Save(_config);

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}