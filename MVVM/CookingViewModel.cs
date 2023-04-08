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

		public Visibility AutocompletePopupVisibility
		{
			get
			{
				if (SearchQuery.Length > 0) return Visibility.Visible;
				return Visibility.Hidden;
			}
		}
		string searchQuery;
		public string SearchQuery
		{
			get
			{
				return searchQuery;
			}
			set
			{
				searchQuery = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Ingredient>  SelectedIngredients { get; set; }
		private void GetAutocomplete()
		{
			var apiInstance = new IngredientsApi();
			if (searchQuery.Length <= 0) return;
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
		public RelayCommand IngredientSelectedCommand { get; set; }
		public CookingViewModel()
		{
			SearchQuery = "";
			SelectedIngredients = new ObservableCollection<Ingredient>();
			SearchChangedCommand = new RelayCommand((o) =>
			{
				GetAutocomplete();
				OnPropertyChanged(nameof(AutocompletePopupVisibility));
			});
			IngredientSelectedCommand = new RelayCommand((selectedIndex) =>
			{
				if ((int)selectedIndex < 0) return;
				Ingredient ing = SuggestedIngredients[(int)selectedIndex];
				SelectedIngredients.Add(ing);
				SearchQuery = "";
				OnPropertyChanged(nameof(AutocompletePopupVisibility));
				SuggestedIngredients.Clear();
				

			});
		}

		
	}
}
