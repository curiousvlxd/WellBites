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
using WellBites.Models;
using WellBites.MVVM.ViewModels;
using WellBites.MVVM.Views;

namespace WellBites
{
    /// <summary>
    /// Interaction logic for CookingPage.xaml
    /// </summary>
    public partial class CookingPage : Page
	{
		public CookingPage()
		{
			InitializeComponent();
			var page  = new RecipeDetailsPage();
			var vm = ((CookingViewModel)this.DataContext).RecipeDetailsViewModel;
			vm.page = page;
			page.DataContext = vm;
			recipeDetailsFrame.Content = page;

		}

		private void FontAwesome_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			var vm = ((CookingViewModel)this.DataContext);
			vm.ToggleFavoriteRecipeCommand.Execute((sender as FrameworkElement).DataContext);
		}
	}
}
