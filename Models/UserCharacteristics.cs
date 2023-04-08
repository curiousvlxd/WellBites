using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
	public enum Sex
	{
		Male,
		Female
	}
	public enum Activity
	{
		BMR, //bare minimum needed to stay alive
		Sedentary, //no exercise 
		Light, //1-3 times/week
		Moderate, //4-5 times/week
		Active, //daily exercise or intense exercise 3-4 times/week
		VeryActive, //intense exercise 6-7 times/week
		ExtraActive //very intense exercise daily or physical job
	}
	public class UserCharacteristics
	{	
		public int Id { get; set; }
		public int Weight { get; set; }
		public int Height { get; set; }
		public Sex Sex { get; set; }
		public int Age { get; set; }
		public Activity Activity { get; set; }
        public User User { get; set; }

    }
}
