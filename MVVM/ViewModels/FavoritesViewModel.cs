using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Core;
using WellBites.Models;

namespace WellBites.MVVM.ViewModels
{
	internal class FavoritesViewModel:ObservableObject
	{
		public ObservableCollection<Recipe> Recipes { get; set; }
		public FavoritesViewModel()
		{

		}
	}
}
