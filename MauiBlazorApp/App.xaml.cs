using Microsoft.Extensions.Configuration;

namespace MauiBlazorApp
{
    public partial class App : Application
    {

        private static IConfigurationSection _configSection { get; set; }

        public App(MainPage page)
        {
            InitializeComponent();

            _configSection = page.Configuration.GetSection("Settings");

            MainPage = page;
        }

        public static string GetConfigSectionValue(string key)
        {
            return _configSection.GetValue<string>(key);
        }
    }
}