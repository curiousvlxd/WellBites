using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
    internal class FavoritesList
    {
        static FavoritesList instance;
        public static FavoritesList Instance
        {
            get
            {
                if (instance == null) instance = new FavoritesList();
                return instance;
            }

        }
        private FavoritesList()
        {
            Recipes = new List<Recipe>();
        }
        public List<Recipe> Recipes { get; set; }
    }
}