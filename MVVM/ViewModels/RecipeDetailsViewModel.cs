using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WellBites.Core;
using WellBites.Models;
using WellBites.MVVM.Views;

namespace WellBites.MVVM.ViewModels
{
	internal class RecipeDetailsViewModel:ObservableObject
	{
		Recipe viewedRecipe;

		public Page page; //because screw mvvm
		

		public Recipe ViewedRecipe { get
			{
				return viewedRecipe;
			}
			set
			{
				viewedRecipe = value;
				OnPropertyChanged();
			}
		}
		
	}
}
