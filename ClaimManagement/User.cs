using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimManagement
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; } //place holder for lecturer, admin etc
        public DateTime CreatedDate { get; set; }

        //i got this code structure from stack overflow
        //https://stackoverflow.com/questions/5096926/what-is-the-get-set-syntax-in-c
        //one noa
        //https://stackoverflow.com/users/3082718/one-noa

    }
}
