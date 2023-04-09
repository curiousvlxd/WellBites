using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WellBites.Core;
using WellBites.Models;

namespace WellBites.MVVM.ViewModels
{
	internal class FavoritesViewModel:ObservableObject
	{
		public FavoritesList FavoritesList { get; set; }
		
		Recipe selectedRecipe;
		public Recipe SelectedRecipe
		{
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
		public RecipeDetailsViewModel RecipeDetailsViewModel
		{
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

		public Visibility NoFavsTextVisibility { get
			{
				return FavoritesList.Recipes.Count < 1?Visibility.Visible:Visibility.Hidden;
			} }
		public FavoritesViewModel()
		{
		
			FavoritesList = FavoritesList.Instance;
			//recipeDetailsViewModel.ViewedRecipe = FavoritesList.Recipes[0];
			RecipeSelectedCommand = new RelayCommand((selectedIndex) =>
			{
				
				if ((int)selectedIndex < 0 || (int)selectedIndex>=FavoritesList.Recipes.Count) return;
				
				Recipe selected = FavoritesList.Recipes[(int)selectedIndex];
				SelectedRecipe = selected;
				//RightSideTopText = selected.Title;
				selected.PopulateDetails();
				RecipeDetailsViewModel.ViewedRecipe = SelectedRecipe;
				
				OnPropertyChanged(nameof(SelectedRecipe));
				OnPropertyChanged(nameof(RecipeDetailsViewModel));
				OnPropertyChanged(nameof(RecipeDetailsViewModel.ViewedRecipe));
				RecipeDetailsVisibility = Visibility.Visible;
				

			});
			
			ToggleFavoriteRecipeCommand = new RelayCommand((recipe) =>
			{
				Recipe rec = (Recipe)recipe;
				rec.IsFavorite = !rec.IsFavorite;

				if (rec.IsFavorite)
				{
					FavoritesList.Recipes.Add(rec);
				}
				else
				{
					FavoritesList.Recipes.Remove(rec);
				}
				OnPropertyChanged(nameof(FavoritesList));
				OnPropertyChanged(nameof(FavoritesList.Recipes));
			});


			BackToFavListCommand = new RelayCommand((o) =>
			{
				RecipeDetailsVisibility = Visibility.Collapsed;
				
			});

			recipeDetailsVisibility = Visibility.Hidden;
			RecipeDetailsViewModel = new RecipeDetailsViewModel();
		}

		public RelayCommand RecipeSelectedCommand { get; set; }
		public RelayCommand ToggleFavoriteRecipeCommand { get; set; }
		public RelayCommand BackToFavListCommand { get; set; }
		Visibility recipeDetailsVisibility;
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

	}
}
