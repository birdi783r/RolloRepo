using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessModel
{
    public class UserModel
    {
        public UserModel()
        {

        }
        public UserModel(int iD, string email, string firstName, string lastName, bool isAdmin, bool isSuperAdmin, string password)
        {
            ID = iD;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
            IsSuperAdmin = isSuperAdmin;
            Password = password;
        } 
        
        public UserModel(string email, string firstName, string lastName, bool isAdmin, bool isSuperAdmin, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
            IsSuperAdmin = isSuperAdmin;
            Password = password;
        }

        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Password { get; set; }
    }
}
