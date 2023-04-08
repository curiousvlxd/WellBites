using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.DataAccess;
using WellBites.Models;

namespace WellBites.MVVM.ViewModels
{
    class UserViewModel
    {   

        public UserManagerService UserManagerService { get; set; }
        public User User { get; set; }

        public UserCharacteristics UserCharacteristics { get; set; }

        public UserViewModel()
        {
            User = new User();
            UserCharacteristics = new UserCharacteristics();
        }

        public UserViewModel(UserManagerService userManagerService)
        {
            UserManagerService = userManagerService;
            User = new User();
            UserCharacteristics = new UserCharacteristics();
        }

    }
}
