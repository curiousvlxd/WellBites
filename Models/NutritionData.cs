using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
    public class NutritionData
    {
        public Guid Id { get; set; }
        public string Calories { get; set; }
        public string Carbs { get; set; }
        public string Fat { get; set; }
        public string Protein { get; set; }
    }
}
