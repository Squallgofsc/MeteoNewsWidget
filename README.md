# MeteoNewsWidget

![Version](https://img.shields.io/badge/version-1.0.0-blue)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![Framework](https://img.shields.io/badge/.NET-10.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 🌍 Choose your language / Choisissez votre langue

- [🇫🇷 Français](#-français)
- [🇬🇧 English](#-english)
- [🇪🇸 Español](#-español)
- [🇩🇪 Deutsch](#-deutsch)
- [🇮🇹 Italiano](#-italiano)
- [🇵🇹 Português](#-português)

---

## 🇫🇷 Français

### Description
MeteoNewsWidget est un widget de bureau natif Windows affichant en temps réel la météo de votre ville, les dernières actualités filtrables par catégorie, et les dernières modifications de vos pages Wikipedia surveillées — le tout dans une interface élégante et semi-transparente.

### Fonctionnalités
- 🌤️ **Météo en temps réel** : température, description, icône météo, prévisions J+1 et J+2
- 📰 **Actualités** : 4 filtres (Local, National, International, Thème personnalisable), rotation automatique toutes les 30 secondes, clic pour ouvrir l'article
- 🌐 **Wikipedia** : article du jour + dernières modifications de vos pages surveillées (si compte renseigné)
- 🗣️ **6 langues** : Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automatique selon la langue choisie
- 📌 **Toujours au premier plan** : option activable/désactivable
- ⚙️ **Paramètres** accessibles depuis le widget

### Prérequis
- Windows 10 ou 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation
1. Téléchargez la dernière version dans [Releases](../../releases)
2. Extrayez l'archive dans le dossier de votre choix
3. Copiez `config.example.json` et renommez la copie en `config.json`
4. Ouvrez `config.json` et renseignez vos informations (voir Configuration)
5. Lancez `MeteoNewsWidget.exe`

### Configuration
Ouvrez `config.json` (ou utilisez le bouton ⚙️ dans le widget) :

```json
{
  "City": "Paris",
  "OpenWeatherApiKey": "VOTRE_CLE_API",
  "Units": "metric",
  "Language": "fr",
  "NewsThemeKeyword": "technologie",
  "WikiUsername": "",
  "WikiToken": "",
  "AlwaysOnTop": true
}
```

| Champ | Description |
|-------|-------------|
| `City` | Nom de votre ville (ex: `Paris`, `Lyon,FR`) |
| `OpenWeatherApiKey` | Clé API gratuite sur [openweathermap.org](https://openweathermap.org/api) |
| `Units` | `metric` (°C) ou `imperial` (°F) |
| `Language` | Code langue : `fr`, `en`, `es`, `de`, `it`, `pt` |
| `NewsThemeKeyword` | Mot-clé pour le filtre Thème |
| `WikiUsername` | Votre nom d'utilisateur Wikipedia (optionnel) |
| `WikiToken` | Token RSS de votre liste de suivi Wikipedia (optionnel) |
| `AlwaysOnTop` | `true` = toujours au premier plan |

### Obtenir une clé API OpenWeatherMap
1. Créez un compte gratuit sur [openweathermap.org](https://openweathermap.org)
2. Allez dans **My API Keys**
3. Copiez votre clé et collez-la dans `config.json`
> ⚠️ La clé peut mettre jusqu'à 2h pour être activée après création.

### Obtenir le token Wikipedia (optionnel)
1. Connectez-vous à votre compte Wikipedia
2. Allez dans **Préférences** → **Liste de suivi** → **Gérer les tokens**
3. Copiez le token et collez-le dans `config.json`
> Sans token, le widget affiche uniquement l'article du jour Wikipedia.

### Compilation depuis les sources
```bash
git clone https://github.com/VOTRE_USERNAME/MeteoNewsWidget.git
cd MeteoNewsWidget
copy config.example.json config.json
dotnet build
dotnet run
```

### Historique des versions
Voir la section [📋 Changelog](#-changelog--historique-des-versions) en bas de ce fichier.

---

## 🇬🇧 English

### Description
MeteoNewsWidget is a native Windows desktop widget displaying real-time weather for your city, the latest news filterable by category, and the latest changes to your watched Wikipedia pages — all in an elegant semi-transparent interface.

### Features
- 🌤️ **Real-time weather**: temperature, description, weather icon, tomorrow and day-after-tomorrow forecasts
- 📰 **News**: 4 filters (Local, National, International, Custom Theme), automatic rotation every 30 seconds, click to open article
- 🌐 **Wikipedia**: article of the day + latest changes to your watched pages (if account configured)
- 🗣️ **6 languages**: Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automatic based on selected language
- 📌 **Always on top**: toggle option
- ⚙️ **Settings** accessible from the widget

### Requirements
- Windows 10 or 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation
1. Download the latest release from [Releases](../../releases)
2. Extract the archive to your preferred folder
3. Copy `config.example.json` and rename the copy to `config.json`
4. Open `config.json` and fill in your information (see Configuration)
5. Launch `MeteoNewsWidget.exe`

### Configuration
Open `config.json` (or use the ⚙️ button in the widget):

```json
{
  "City": "London",
  "OpenWeatherApiKey": "YOUR_API_KEY",
  "Units": "imperial",
  "Language": "en",
  "NewsThemeKeyword": "technology",
  "WikiUsername": "",
  "WikiToken": "",
  "AlwaysOnTop": true
}
```

### Getting an OpenWeatherMap API Key
1. Create a free account at [openweathermap.org](https://openweathermap.org)
2. Go to **My API Keys**
3. Copy your key and paste it into `config.json`
> ⚠️ The key may take up to 2 hours to activate after creation.

### Getting a Wikipedia Token (optional)
1. Log in to your Wikipedia account
2. Go to **Preferences** → **Watchlist** → **Manage tokens**
3. Copy the token and paste it into `config.json`
> Without a token, the widget only displays the Wikipedia article of the day.

### Build from source
```bash
git clone https://github.com/YOUR_USERNAME/MeteoNewsWidget.git
cd MeteoNewsWidget
copy config.example.json config.json
dotnet build
dotnet run
```

### Version history
See the [📋 Changelog](#-changelog--historique-des-versions) section at the bottom of this file.

---

## 🇪🇸 Español

### Descripción
MeteoNewsWidget es un widget de escritorio nativo de Windows que muestra en tiempo real el clima de tu ciudad, las últimas noticias filtrables por categoría y los últimos cambios en tus páginas de Wikipedia vigiladas, todo en una interfaz elegante y semitransparente.

### Características
- 🌤️ **Clima en tiempo real**: temperatura, descripción, icono del clima, previsiones para mañana y pasado mañana
- 📰 **Noticias**: 4 filtros (Local, Nacional, Internacional, Tema personalizable), rotación automática cada 30 segundos, clic para abrir el artículo
- 🌐 **Wikipedia**: artículo del día + últimos cambios en tus páginas vigiladas (si la cuenta está configurada)
- 🗣️ **6 idiomas**: Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automático según el idioma seleccionado
- 📌 **Siempre encima**: opción activable/desactivable
- ⚙️ **Configuración** accesible desde el widget

### Requisitos
- Windows 10 u 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Instalación
1. Descarga la última versión en [Releases](../../releases)
2. Extrae el archivo en la carpeta de tu elección
3. Copia `config.example.json` y renombra la copia como `config.json`
4. Abre `config.json` e introduce tu información (ver Configuración)
5. Ejecuta `MeteoNewsWidget.exe`

### Obtener una clave API de OpenWeatherMap
1. Crea una cuenta gratuita en [openweathermap.org](https://openweathermap.org)
2. Ve a **My API Keys**
3. Copia tu clave y pégala en `config.json`
> ⚠️ La clave puede tardar hasta 2 horas en activarse tras su creación.

### Obtener un token de Wikipedia (opcional)
1. Inicia sesión en tu cuenta de Wikipedia
2. Ve a **Preferencias** → **Lista de seguimiento** → **Gestionar tokens**
3. Copia el token y pégalo en `config.json`
> Sin token, el widget solo muestra el artículo del día de Wikipedia.

### Historial de versiones
Ver la sección [📋 Changelog](#-changelog--historique-des-versions) al final de este archivo.

---

## 🇩🇪 Deutsch

### Beschreibung
MeteoNewsWidget ist ein natives Windows-Desktop-Widget, das in Echtzeit das Wetter Ihrer Stadt, die neuesten nach Kategorie filterbaren Nachrichten und die letzten Änderungen an Ihren beobachteten Wikipedia-Seiten anzeigt — alles in einer eleganten, halbtransparenten Oberfläche.

### Funktionen
- 🌤️ **Echtzeit-Wetter**: Temperatur, Beschreibung, Wetter-Icon, Vorhersagen für morgen und übermorgen
- 📰 **Nachrichten**: 4 Filter (Lokal, National, International, Benutzerdefiniertes Thema), automatische Rotation alle 30 Sekunden, Klick zum Öffnen des Artikels
- 🌐 **Wikipedia**: Artikel des Tages + letzte Änderungen an beobachteten Seiten (wenn Konto konfiguriert)
- 🗣️ **6 Sprachen**: Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automatisch je nach gewählter Sprache
- 📌 **Immer im Vordergrund**: ein-/ausschaltbare Option
- ⚙️ **Einstellungen** direkt im Widget zugänglich

### Voraussetzungen
- Windows 10 oder 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation
1. Laden Sie die neueste Version unter [Releases](../../releases) herunter
2. Entpacken Sie das Archiv in einen Ordner Ihrer Wahl
3. Kopieren Sie `config.example.json` und benennen Sie die Kopie in `config.json` um
4. Öffnen Sie `config.json` und tragen Sie Ihre Informationen ein (siehe Konfiguration)
5. Starten Sie `MeteoNewsWidget.exe`

### OpenWeatherMap API-Schlüssel erhalten
1. Erstellen Sie ein kostenloses Konto auf [openweathermap.org](https://openweathermap.org)
2. Gehen Sie zu **My API Keys**
3. Kopieren Sie Ihren Schlüssel und fügen Sie ihn in `config.json` ein
> ⚠️ Der Schlüssel kann nach der Erstellung bis zu 2 Stunden brauchen, um aktiviert zu werden.

### Wikipedia-Token erhalten (optional)
1. Melden Sie sich bei Ihrem Wikipedia-Konto an
2. Gehen Sie zu **Einstellungen** → **Beobachtungsliste** → **Token verwalten**
3. Kopieren Sie den Token und fügen Sie ihn in `config.json` ein
> Ohne Token zeigt das Widget nur den Wikipedia-Artikel des Tages an.

### Versionsgeschichte
Siehe den Abschnitt [📋 Changelog](#-changelog--historique-des-versions) am Ende dieser Datei.

---

## 🇮🇹 Italiano

### Descrizione
MeteoNewsWidget è un widget desktop nativo per Windows che mostra in tempo reale il meteo della tua città, le ultime notizie filtrabili per categoria e le ultime modifiche alle tue pagine Wikipedia monitorate, il tutto in un'interfaccia elegante e semitrasparente.

### Funzionalità
- 🌤️ **Meteo in tempo reale**: temperatura, descrizione, icona meteo, previsioni per domani e dopodomani
- 📰 **Notizie**: 4 filtri (Locale, Nazionale, Internazionale, Tema personalizzabile), rotazione automatica ogni 30 secondi, clic per aprire l'articolo
- 🌐 **Wikipedia**: articolo del giorno + ultime modifiche alle pagine monitorate (se l'account è configurato)
- 🗣️ **6 lingue**: Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automatico in base alla lingua selezionata
- 📌 **Sempre in primo piano**: opzione attivabile/disattivabile
- ⚙️ **Impostazioni** accessibili dal widget

### Requisiti
- Windows 10 o 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installazione
1. Scarica l'ultima versione da [Releases](../../releases)
2. Estrai l'archivio nella cartella desiderata
3. Copia `config.example.json` e rinomina la copia in `config.json`
4. Apri `config.json` e inserisci le tue informazioni (vedi Configurazione)
5. Avvia `MeteoNewsWidget.exe`

### Ottenere una chiave API OpenWeatherMap
1. Crea un account gratuito su [openweathermap.org](https://openweathermap.org)
2. Vai su **My API Keys**
3. Copia la tua chiave e incollala in `config.json`
> ⚠️ La chiave può impiegare fino a 2 ore per essere attivata dopo la creazione.

### Ottenere un token Wikipedia (opzionale)
1. Accedi al tuo account Wikipedia
2. Vai su **Preferenze** → **Lista di controllo** → **Gestisci token**
3. Copia il token e incollalo in `config.json`
> Senza token, il widget mostra solo l'articolo del giorno di Wikipedia.

### Cronologia versioni
Vedere la sezione [📋 Changelog](#-changelog--historique-des-versions) in fondo a questo file.

---

## 🇵🇹 Português

### Descrição
MeteoNewsWidget é um widget de ambiente de trabalho nativo para Windows que exibe em tempo real o tempo da sua cidade, as últimas notícias filtráveis por categoria e as últimas modificações nas suas páginas Wikipedia vigiadas — tudo numa interface elegante e semitransparente.

### Funcionalidades
- 🌤️ **Tempo em tempo real**: temperatura, descrição, ícone do tempo, previsões para amanhã e depois de amanhã
- 📰 **Notícias**: 4 filtros (Local, Nacional, Internacional, Tema personalizado), rotação automática a cada 30 segundos, clique para abrir o artigo
- 🌐 **Wikipedia**: artigo do dia + últimas modificações nas páginas vigiadas (se a conta estiver configurada)
- 🗣️ **6 idiomas**: Français, English, Español, Deutsch, Italiano, Português
- 🌡️ **°C / °F** automático conforme o idioma selecionado
- 📌 **Sempre à frente**: opção ativável/desativável
- ⚙️ **Definições** acessíveis a partir do widget

### Requisitos
- Windows 10 ou 11
- [.NET 10.0 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

### Instalação
1. Descarregue a última versão em [Releases](../../releases)
2. Extraia o arquivo para a pasta da sua escolha
3. Copie `config.example.json` e renomeie a cópia para `config.json`
4. Abra `config.json` e preencha as suas informações (ver Configuração)
5. Execute `MeteoNewsWidget.exe`

### Obter uma chave API OpenWeatherMap
1. Crie uma conta gratuita em [openweathermap.org](https://openweathermap.org)
2. Vá a **My API Keys**
3. Copie a sua chave e cole-a em `config.json`
> ⚠️ A chave pode demorar até 2 horas a ser ativada após a criação.

### Obter um token Wikipedia (opcional)
1. Inicie sessão na sua conta Wikipedia
2. Vá a **Preferências** → **Lista de acompanhamento** → **Gerir tokens**
3. Copie o token e cole-o em `config.json`
> Sem token, o widget apenas apresenta o artigo do dia da Wikipedia.

### Histórico de versões
Ver a secção [📋 Changelog](#-changelog--historique-des-versions) no final deste ficheiro.

---

## 📋 Changelog / Historique des versions

### v1.0.0 — 2026

---

#### 🇫🇷 Français — Version initiale

**Météo**
- Affichage de la météo en temps réel via l'API OpenWeatherMap
- Température actuelle, description, icône météo locale (9 icônes incluses)
- Prévisions pour J+1 et J+2
- Changement de ville directement depuis le widget (sans éditer config.json)
- Affichage automatique en °C ou °F selon la langue choisie

**Actualités**
- Flux RSS Google News avec 4 filtres : Local, National, International, Thème
- Le filtre National s'adapte automatiquement au pays de la ville choisie
- Rotation automatique des articles toutes les 30 secondes
- Info-bulle affichant le titre complet au survol
- Clic sur un article pour l'ouvrir dans le navigateur par défaut

**Wikipedia**
- Article du jour Wikipedia (édition selon la langue configurée)
- Dernières modifications des pages surveillées (nécessite un compte Wikipedia)
- Logo Wikipedia officiel affiché
- Lien cliquable vers chaque article

**Interface**
- Fenêtre semi-transparente, sans bordure, déplaçable par glisser-déposer
- Option "Toujours au premier plan" activable/désactivable
- Fenêtre de paramètres intégrée avec sélecteur de langue
- Redimensionnable

**Langues**
- Français, English, Español, Deutsch, Italiano, Português
- Interface entièrement traduite dans chaque langue
- URLs des actualités adaptées à la langue et au pays

---

#### 🇬🇧 English — Initial release

**Weather**
- Real-time weather display via OpenWeatherMap API
- Current temperature, description, local weather icon (9 icons included)
- Forecasts for tomorrow and the day after tomorrow
- Change city directly from the widget (without editing config.json)
- Automatic °C or °F display based on selected language

**News**
- Google News RSS feed with 4 filters: Local, National, International, Theme
- National filter automatically adapts to the country of the selected city
- Automatic article rotation every 30 seconds
- Tooltip showing full title on hover
- Click an article to open it in the default browser

**Wikipedia**
- Wikipedia article of the day (edition based on configured language)
- Latest changes to watched pages (requires a Wikipedia account)
- Official Wikipedia logo displayed
- Clickable link to each article

**Interface**
- Semi-transparent, borderless window, draggable by click-and-drag
- "Always on top" option, toggle on/off
- Built-in settings window with language selector
- Resizable

**Languages**
- Français, English, Español, Deutsch, Italiano, Português
- Interface fully translated in each language
- News URLs adapted to language and country

---

#### 🇪🇸 Español — Versión inicial

**Clima**
- Visualización del clima en tiempo real a través de la API de OpenWeatherMap
- Temperatura actual, descripción, icono del clima local (9 iconos incluidos)
- Previsiones para mañana y pasado mañana
- Cambio de ciudad directamente desde el widget (sin editar config.json)
- Visualización automática en °C o °F según el idioma seleccionado

**Noticias**
- Feed RSS de Google News con 4 filtros: Local, Nacional, Internacional, Tema
- El filtro Nacional se adapta automáticamente al país de la ciudad seleccionada
- Rotación automática de artículos cada 30 segundos
- Información emergente con el título completo al pasar el ratón
- Clic en un artículo para abrirlo en el navegador predeterminado

**Wikipedia**
- Artículo del día de Wikipedia (edición según el idioma configurado)
- Últimos cambios en páginas vigiladas (requiere cuenta de Wikipedia)
- Logo oficial de Wikipedia mostrado
- Enlace clicable a cada artículo

**Interfaz**
- Ventana semitransparente, sin bordes, arrastrable
- Opción "Siempre encima" activable/desactivable
- Ventana de configuración integrada con selector de idioma
- Redimensionable

---

#### 🇩🇪 Deutsch — Erste Version

**Wetter**
- Echtzeit-Wetteranzeige über die OpenWeatherMap-API
- Aktuelle Temperatur, Beschreibung, lokales Wetter-Icon (9 Icons enthalten)
- Vorhersagen für morgen und übermorgen
- Stadt direkt im Widget ändern (ohne config.json zu bearbeiten)
- Automatische °C- oder °F-Anzeige je nach gewählter Sprache

**Nachrichten**
- Google News RSS-Feed mit 4 Filtern: Lokal, National, International, Thema
- Der nationale Filter passt sich automatisch dem Land der gewählten Stadt an
- Automatische Artikelrotation alle 30 Sekunden
- Tooltip mit vollständigem Titel beim Hover
- Klick auf einen Artikel öffnet ihn im Standardbrowser

**Wikipedia**
- Wikipedia-Artikel des Tages (Ausgabe je nach konfigurierter Sprache)
- Letzte Änderungen an beobachteten Seiten (Wikipedia-Konto erforderlich)
- Offizielles Wikipedia-Logo angezeigt
- Klickbarer Link zu jedem Artikel

**Oberfläche**
- Halbtransparentes, rahmenloses Fenster, per Drag-and-Drop verschiebbar
- Option "Immer im Vordergrund" ein-/ausschaltbar
- Integriertes Einstellungsfenster mit Sprachauswahl
- Größenänderbar

---

#### 🇮🇹 Italiano — Versione iniziale

**Meteo**
- Visualizzazione del meteo in tempo reale tramite l'API OpenWeatherMap
- Temperatura attuale, descrizione, icona meteo locale (9 icone incluse)
- Previsioni per domani e dopodomani
- Cambio città direttamente dal widget (senza modificare config.json)
- Visualizzazione automatica in °C o °F in base alla lingua selezionata

**Notizie**
- Feed RSS di Google News con 4 filtri: Locale, Nazionale, Internazionale, Tema
- Il filtro Nazionale si adatta automaticamente al paese della città selezionata
- Rotazione automatica degli articoli ogni 30 secondi
- Tooltip con titolo completo al passaggio del mouse
- Clic su un articolo per aprirlo nel browser predefinito

**Wikipedia**
- Articolo del giorno di Wikipedia (edizione in base alla lingua configurata)
- Ultime modifiche alle pagine monitorate (richiede un account Wikipedia)
- Logo ufficiale di Wikipedia visualizzato
- Link cliccabile a ogni articolo

**Interfaccia**
- Finestra semitrasparente, senza bordi, trascinabile
- Opzione "Sempre in primo piano" attivabile/disattivabile
- Finestra delle impostazioni integrata con selettore di lingua
- Ridimensionabile

---

#### 🇵🇹 Português — Versão inicial

**Tempo**
- Exibição do tempo em tempo real através da API OpenWeatherMap
- Temperatura atual, descrição, ícone do tempo local (9 ícones incluídos)
- Previsões para amanhã e depois de amanhã
- Alteração da cidade diretamente no widget (sem editar config.json)
- Exibição automática em °C ou °F conforme o idioma selecionado

**Notícias**
- Feed RSS do Google News com 4 filtros: Local, Nacional, Internacional, Tema
- O filtro Nacional adapta-se automaticamente ao país da cidade selecionada
- Rotação automática de artigos a cada 30 segundos
- Dica de ferramenta com título completo ao passar o rato
- Clique num artigo para o abrir no navegador predefinido

**Wikipedia**
- Artigo do dia da Wikipedia (edição conforme o idioma configurado)
- Últimas modificações nas páginas vigiadas (requer conta Wikipedia)
- Logótipo oficial da Wikipedia apresentado
- Ligação clicável para cada artigo

**Interface**
- Janela semitransparente, sem bordas, arrastável
- Opção "Sempre à frente" ativável/desativável
- Janela de definições integrada com seletor de idioma
- Redimensionável

---

## 👤 Auteur / Author

**Squallgofsc**
Développé avec l'assistance de / Developed with the assistance of **Claude** (Anthropic)

---

## 🙏 Remerciements / Acknowledgements

- [OpenWeatherMap](https://openweathermap.org) — API météo / Weather API
- [Google News RSS](https://news.google.com) — Flux d'actualités / News feed
- [Wikipedia](https://www.wikipedia.org) — API et données encyclopédiques / API and encyclopedic data
- [Wikimedia Commons](https://commons.wikimedia.org) — Logo Wikipedia

---

## 📄 Licence / License

MIT License — voir [LICENSE](LICENSE) pour les détails / see [LICENSE](LICENSE) for details.
