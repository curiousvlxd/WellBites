using WellBites.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WellBites.Helpers;
using com.spoonacular;
using MaterialDesignThemes.Wpf;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using WellBites.Views;
using Color = System.Drawing.Color;

namespace WellBites
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        private AuthPage _authPage;
        public MainWindow(AuthPage authPage, string apiKey)
        {
            InitializeComponent();
            _authPage = authPage;
            WindowHelper._this = this;
            this.SourceInitialized += new EventHandler(WindowHelper.win_SourceInitialized);
			Configuration.ApiKey.Add("x-api-key", $"{apiKey}");
		}

        private void OnThemesClick(object sender, RoutedEventArgs e)
        {
            ThemesController.SetTheme(ThemesToggleButton.IsChecked == true
                ? ThemesController.ThemeTypes.Dark
                : ThemesController.ThemeTypes.Light);
        }

        private void OnBtnCloseClick(object sender, RoutedEventArgs e)
        {
            WindowHelper.Exit();
        }

        private void OnBtnFullScreenClick(object sender, RoutedEventArgs e)
        {
            WindowHelper.DoFullscreen();
        }

        private void OnBtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowHelper.Minimize();
        }

        private void OnTitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.MoveWindow(e);
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Height == 800 && this.Width == 1000)
            {
                if (this.WindowState == WindowState.Maximized) BorderMain.Margin = new Thickness(0);
                else BorderMain.Margin = new Thickness(30);
            }
            else BorderMain.Margin = new Thickness(0);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (WindowHelper.ShouldSystemUseDarkMode())
            {
                ThemesToggleButton.IsChecked = true;
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark); 
            }

            FrameMain.Content = _authPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
		{
			var apiInstance = new RecipesApi();
			var ingredients = "carrots, tomatoes";  // string | A comma-separated list of ingredients that the recipes should contain. (optional) 
			var number = 10;  // int? | The maximum number of items to return (between 1 and 100). Defaults to 10. (optional)  (default to 10)
			var limitLicense = true;  // bool? | Whether the recipes should have an open license that allows display with proper attribution. (optional)  (default to true)
			var ranking = 1;  // decimal? | Whether to maximize used ingredients (1) or minimize missing ingredients (2) first. (optional) 
			var ignorePantry = false;  // bool? | Whether to ignore typical pantry items, such as water, salt, flour, etc. (optional)  (default to false)

			try
			{
				// Search Recipes by Ingredients
				List <SearchRecipesByIngredients200ResponseInner> result = apiInstance.SearchRecipesByIngredients(ingredients, number, limitLicense, ranking, ignorePantry);
				MessageBox.Show(result[0].ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
