using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.DataAccess;
using WellBites.Models;

namespace WellBites.MVVM.ViewModels
{
    public class UserViewModel
    {   

        public UserManagerService UserManagerService { get; set; }
        public User User { get; set; }
        public UserViewModel(UserManagerService userManagerService)
        {
            UserManagerService = userManagerService;
            User = new User();
        }

    }
}
