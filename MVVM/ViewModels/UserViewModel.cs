using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Models;

namespace WellBites.MVVM.ViewModels
{
    class UserViewModel
    {
        public User User { get; set; }

        public UserCharacteristics UserCharacteristics { get; set; }

        public UserViewModel()
        {
            User = new User();
            UserCharacteristics = new UserCharacteristics();
        }

        public UserViewModel(User user, UserCharacteristics userCharacteristics)
        {
            User = user;
            UserCharacteristics = userCharacteristics;
        }

    }
}
