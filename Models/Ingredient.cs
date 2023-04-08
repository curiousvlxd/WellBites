using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
	internal class Ingredient
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
		public string ImageURL
		{
			get { 
				string url = "https://spoonacular.com/cdn/ingredients_100x100/" + Image;
				return url;

			}
		}
	}
}
