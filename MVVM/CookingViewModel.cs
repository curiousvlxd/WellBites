using com.spoonacular;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WellBites.Core;
using WellBites.Models;

namespace WellBites.MVVM
{
	internal class CookingViewModel:ObservableObject
	{
		ObservableCollection<Ingredient> suggestedIngredients;
		public ObservableCollection<Ingredient> SuggestedIngredients { get
			{
				return suggestedIngredients;
			}
			set
			{
				suggestedIngredients = value;
				OnPropertyChanged();
			}
			}
		public RelayCommand SearchChangedCommand { get; set; }
		public string SearchQuery { get; set; }

		private void GetAutocomplete()
		{
			var apiInstance = new IngredientsApi();

			try
			{
				// Search Recipes by Ingredients
				List<AutocompleteIngredientSearch200ResponseInner> response = apiInstance.AutocompleteIngredientSearch(SearchQuery, 10, false, "", "en");
				IEnumerable<Ingredient> result;
				result = response.Select(ing => new Ingredient() { Name = ing.Name, Image = ing.Image }); //map to our own model type
				SuggestedIngredients = new ObservableCollection<Ingredient>(result);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public CookingViewModel()
		{
			SearchChangedCommand = new RelayCommand((o) =>
			{
				GetAutocomplete();
			});
		}

		
	}
}
