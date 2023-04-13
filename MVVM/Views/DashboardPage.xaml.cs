using System;
using System.Collections.Generic;
using System.Linq;
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
using WellBites.MVVM.ViewModels;

namespace WellBites.MVVM.Views
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
	{	
		private UserViewModel _userViewModel;

		private CookingViewModel cookingvm = new CookingViewModel();
		private FavoritesViewModel favvm = new FavoritesViewModel();

		public DashboardPage(UserViewModel userViewModel)
		{
			InitializeComponent();
			_userViewModel = userViewModel;
		}

		private void findOption_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//frame.Content = new CookingPage();
			frame.Content = new CookingPage();
			frame.DataContext = cookingvm;
		}

		private void favoritesOption_MouseDown(object sender, MouseButtonEventArgs e)
		{
			frame.Content = new FavoritesPage();
			frame.DataContext = favvm;

		}

        private void myCalories_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = new MyCaloriesPage();
            frame.DataContext = _userViewModel;
        }
    }
}
