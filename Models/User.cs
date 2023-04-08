using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellBites.Models
{
	internal class User
	{
		public string Username { get; set; }
		UserCharacteristics Characteristics { get; set; }
	}
}
