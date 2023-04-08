using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Models;

namespace WellBites.DataAccess
{
    public class UserManagerService
    {
        private readonly WellBitesDbContext _dbContext;

        public UserManagerService(WellBitesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.Include(u => u.Characteristics).ToList();
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.Include(u => u.Characteristics).FirstOrDefault(u => u.Id == userId);
        }
    }
}
