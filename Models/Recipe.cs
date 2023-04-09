using com.spoonacular;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Core;
using WellBites.MVVM.Views;

namespace WellBites.Models
{
	internal class Recipe:ObservableObject
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }

		public bool? IsCheap { get; set; }
		public List<string> Diets { get; set; }
		public List<string> Cuisines { get; set; }
		public string Instructions { get; set; }
		public int? CookingTimeInMinutes { get; set; }

		bool isFavorite;
		public bool IsFavorite
		{
			get { return isFavorite; }
			set
			{
				isFavorite = value;
				OnPropertyChanged();
			}
		}
		public string ImageURL
		{
			get
			{
				string url = $"https://spoonacular.com/recipeImages/{Id.ToString()}-240x150.jpg";
				return url;

			}
		}
		public string BigImageURL
		{
			get
			{
				string url = $"https://spoonacular.com/recipeImages/{Id.ToString()}-312x231.jpg";
				return url;

			}
		}
		public override string ToString()
		{
			return Title;
		}
		public Recipe()
		{
			Id = 0;
			Title = "untitled recipe";

		}
		public void PopulateDetails()
		{
			RecipesApi apiInstance = new RecipesApi();
			GetRecipeInformation200Response response = apiInstance.GetRecipeInformation(Id, true);
			IsCheap = response.Cheap;
			Cuisines = response.Cuisines;
			Diets = response.Diets;
			CookingTimeInMinutes = response.ReadyInMinutes;
			Instructions = response.Instructions;
			Instructions = Instructions.Replace("<ol>", "");
			Instructions = Instructions.Replace("</ol>", "");
			Instructions = Instructions.Replace("<li>", "- ");
			Instructions = Instructions.Replace("</li>", "\n");

		}
	}
}
