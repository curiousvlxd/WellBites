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
	/// Interaction logic for FavoritesPage.xaml
	/// </summary>
	public partial class FavoritesPage : Page
	{
		public FavoritesPage()
		{
			InitializeComponent();
			var page = new RecipeDetailsPage();
			var vm = ((FavoritesViewModel)this.DataContext).RecipeDetailsViewModel;
			vm.page = page;
			page.DataContext = vm;
			recipeDetailsFrame.Content = page;
		}
		private void FontAwesome_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			var vm = ((FavoritesViewModel)this.DataContext);
			vm.BackToFavListCommand.Execute((sender as FrameworkElement).DataContext);
			
		}

		private void recipesListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((ListBox)sender).SelectedIndex = -1;
		}
	}
}
