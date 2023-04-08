using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
	internal class Recipe
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		public string ImageURL
		{
			get
			{
				string url = $"https://spoonacular.com/recipeImages/{Id.ToString()}-240x150.jpg";
				return url;

			}
		}
		public override string ToString()
		{
			return Title;
		}
	}
}
