using com.spoonacular;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WellBites.Core;
using WellBites.Models;
using WellBites.MVVM.ViewModels;

namespace WellBites.MVVM
{
	internal class CookingViewModel:ObservableObject
	{
		ObservableCollection<Ingredient> suggestedIngredients;
		ObservableCollection<Recipe> foundRecipes;

		string rightSideTopText = "Recipes";
		public string RightSideTopText
		{
			get { return rightSideTopText; }
			set
			{
				rightSideTopText = value;
				OnPropertyChanged();
			}
		}
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



		public ObservableCollection<Recipe> FoundRecipes
		{
			get
			{
				return foundRecipes;
			}
			set
			{
				foundRecipes = value;
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
		Visibility recipeDetailsVisibility;
		Visibility cookButtonVisibility;
		public Visibility RecipeDetailsVisibility
		{
			get
			{
				return recipeDetailsVisibility;

			}
			set
			{
				recipeDetailsVisibility = value;
				OnPropertyChanged();
			}
		}
		public Visibility CookButtonVisibility
		{
			get
			{
				return SelectedIngredients.Count>0?Visibility.Visible:Visibility.Hidden;

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
				Task.Run(() => {
					List<AutocompleteIngredientSearch200ResponseInner> response = apiInstance.AutocompleteIngredientSearch(SearchQuery, 10, false, "", "en");
					IEnumerable<Ingredient> result;
					result = response.Select(ing => new Ingredient() { Name = ing.Name, Image = ing.Image }); //map to our own model type
					SuggestedIngredients = new ObservableCollection<Ingredient>(result);
				});
						  
					  
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private int Denullify(int? n)
		{
			if (n == null) return 0;
			else return (int)n;
		}
		public RelayCommand IngredientSelectedCommand { get; set; }
		public RelayCommand CookCommand { get; set; }
		public RelayCommand RecipeSelectedCommand { get; set; }
		public RelayCommand BackToRecipeListCommand { get; set; }
		public RelayCommand DeleteIngredientCommand { get; set; }
		public RelayCommand ToggleFavoriteRecipeCommand { get; set; }
		public CookingViewModel()
		{
			RecipeDetailsVisibility = Visibility.Hidden;
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
				OnPropertyChanged(nameof(CookButtonVisibility));


			});
			CookCommand = new RelayCommand((o) =>
			{
				if(SelectedIngredients.Count<1)
				{
					MessageBox.Show("You haven't selected any ingredients.", "Can't cook!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
				var apiInstance = new RecipesApi();
				string commaSeparatedIngredients = "";
				foreach (var ing in SelectedIngredients) {
					commaSeparatedIngredients += ing.ToString();
					commaSeparatedIngredients += ",";
				}
				Task.Run(() =>
				{
					List<SearchRecipesByIngredients200ResponseInner> response = apiInstance.SearchRecipesByIngredients(commaSeparatedIngredients, 10, false, 2, false);
					FoundRecipes = new ObservableCollection<Recipe>(response.Select(
						rec => new Recipe()
						{
							Title = rec.Title,
							Id = Denullify(rec.Id),
							MissingIngredients = rec.MissedIngredients
						})); ;;

				});

			});

			RecipeSelectedCommand = new RelayCommand((selectedIndex) =>
			{
				if ((int)selectedIndex < 0) return;
				RecipeDetailsVisibility = Visibility.Visible;
				Recipe selected = FoundRecipes[(int)selectedIndex];
				SelectedRecipe = selected;
				RightSideTopText = selected.Title;
				selected.PopulateDetails();
				RecipeDetailsViewModel.ViewedRecipe = selected;
			});

			BackToRecipeListCommand = new RelayCommand((o) =>
			{
				RecipeDetailsVisibility = Visibility.Hidden;
				RightSideTopText = "Recipes";
			});

			DeleteIngredientCommand = new RelayCommand((ingredient) =>
			{
				SelectedIngredients.Remove((Ingredient)ingredient);
				OnPropertyChanged(nameof(CookButtonVisibility));
			});

			ToggleFavoriteRecipeCommand = new RelayCommand((recipe) =>
			{
				((Recipe)recipe).IsFavorite = !((Recipe)recipe).IsFavorite;

			});

			RecipeDetailsViewModel = new RecipeDetailsViewModel();
		}

		Recipe selectedRecipe;
		public Recipe SelectedRecipe { 
			get
			{
				return selectedRecipe;
			}
			set
			{
				selectedRecipe = value;
				OnPropertyChanged();
			}
		}
		RecipeDetailsViewModel recipeDetailsViewModel;
		public RecipeDetailsViewModel RecipeDetailsViewModel { 
			get
			{
				return recipeDetailsViewModel;
			}
			set
			{
				recipeDetailsViewModel = value;
				OnPropertyChanged();
			}
		}
	}
}
