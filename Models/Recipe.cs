using com.spoonacular;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using WellBites.Core;
using WellBites.MVVM.Views;

namespace WellBites.Models
{
	public class Recipe : ObservableObject
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ApiId { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
        public bool? IsCheap { get; set; }
		public List<string> Diets { get; set; }
		public List<string> Cuisines { get; set; }
		public string Instructions { get; set; }
		public int? CookingTimeInMinutes { get; set; }
        public NutritionData Nutrition { get; set; }

        bool isFavorite;
		public bool IsFavorite
		{
			get { return isFavorite; }
			set
			{
				isFavorite = value;
				//OnPropertyChanged();
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
			ApiId = 0;
			Title = "untitled recipe";
		}

		public string MissingIngredientsString {
			get
			{
				if (MissingIngredients == null) return "";
				if (MissingIngredients.Count == 0) return "You have all ingredients!";
				else
				{
					string str = "Missing ingredients: ";
					foreach (var ing in MissingIngredients)
					{
						str += ing.Name;
						str += ", ";
					}
					str = str.Substring(0, str.Length-2);

					return str;
				}
			}
		
		}

		public bool HasMissingIngredients
		{
			get
			{
				if(MissingIngredients == null) return false;
				return MissingIngredients.Count > 0;
			}
		}
		public string Summary { get; set; }
		public List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner> MissingIngredients { get; set; }

		List<string> missingIngredients;

		public void PopulateDetails()
		{
			PopulateLight();
			RecipesApi apiInstance = new RecipesApi();
			Nutrition = apiInstance.GetRecipeNutritionWidgetByID(Id);

		}
		public void PopulateLight()
		{
			RecipesApi apiInstance = new RecipesApi();
			GetRecipeInformation200Response response = apiInstance.GetRecipeInformation(ApiId, true);
			IsCheap = response.Cheap;
			Cuisines = response.Cuisines;
			Instructions = response.Instructions;
			Diets = response.Diets;
			CookingTimeInMinutes = response.ReadyInMinutes;
			Instructions = response.Instructions;
			Instructions = Instructions.Replace("<ol>", "");
			Instructions = Instructions.Replace("</ol>", "");
			Instructions = Instructions.Replace("<li>", "- ");
			Instructions = Instructions.Replace("</li>", "\n");
			

		var sdkNutrition = apiInstance.GetRecipeNutritionWidgetByID(ApiId);
        Nutrition = new NutritionData();
        Nutrition.Id = Guid.NewGuid();
        Nutrition.Calories = sdkNutrition.Calories;
		Nutrition.Carbs = sdkNutrition.Carbs;
		Nutrition.Fat = sdkNutrition.Fat;
		Nutrition.Protein = sdkNutrition.Protein;
        }
	}
}
