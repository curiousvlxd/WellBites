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

namespace WellBites
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowHelper._this = this;
            this.SourceInitialized += new EventHandler(WindowHelper.win_SourceInitialized);
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
    }
}
