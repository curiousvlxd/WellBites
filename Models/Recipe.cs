using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
	internal class Recipe
	{
		public string Title { get; set; }
		public override string ToString()
		{
			return Title;
		}
	}
}
