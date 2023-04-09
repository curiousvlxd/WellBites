using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
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

    public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public Sex Sex { get; set; }
        public Activity Activity { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-age)) age--;
                return age;
            }
        }

        public void CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();

            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
