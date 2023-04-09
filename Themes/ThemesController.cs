using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace WellBites.Themes
{
    public static class ThemesController
    {
        public static ThemeTypes CurrentTheme { get; set; }

        public enum ThemeTypes
        {
            Light, Dark
        }

        public static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }

        public static void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark: themeName = "Dark"; break;
                case ThemeTypes.Light: themeName = "Light"; break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                {
                    Application.Current.Resources.MergedDictionaries[4].Source = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{themeName}.xaml");   
                    ChangeTheme(new Uri($"Themes/{themeName}Theme.xaml", UriKind.Relative));
                }
            }
            catch { }
        }
    }
}
