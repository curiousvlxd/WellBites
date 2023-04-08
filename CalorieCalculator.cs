using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Models;

namespace WellBites
{
	/*
	 * Mifflin-St Jeor Equation:
For men:
BMR = 10W + 6.25H - 5A + 5
For women:
BMR = 10W + 6.25H - 5A - 161

	where:
W is body weight in kg
H is body height in cm
A is age
F is body fat in percentage 
	 * 
	 */
/*
 * 

If you are sedentary (little or no exercise) : Calorie-Calculation = BMR x 1.2
If you are lightly active (light exercise/sports 1-3 days/week) : Calorie-Calculation = BMR x 1.375
If you are moderatetely active (moderate exercise/sports 3-5 days/week) : Calorie-Calculation = BMR x 1.55
If you are very active (hard exercise/sports 6-7 days a week) : Calorie-Calculation = BMR x 1.725
If you are extra active (very hard exercise/sports & physical job or 2x training) : Calorie-Calculation = BMR x 1.9
*/

internal class CalorieCalculator
{
	public double GetActivityMultiplier(Activity activity)
	{
			switch(activity)
			{
				
				case Activity.Sedentary:
					return 1.2;
				case Activity.Light:
					return 1.375;
				case Activity.Moderate:
					return 1.55;
				case Activity.VeryActive:
					return 1.725;
				case Activity.ExtraActive:
					return 1.9;
				default: //bmr
					return 1;
			}
			
	}
	public int GetCaloriesPerDay(UserCharacteristics characteristics, Activity activity)
	{
		int sex_variable = characteristics.Sex == Sex.Male ? 5 : -161;
		double bmr = 10 * characteristics.Weight + 6.25 * characteristics.Height - 5 * characteristics.Age + sex_variable;
		return (int)Math.Round(GetActivityMultiplier(activity) * bmr);
	}
}
}
