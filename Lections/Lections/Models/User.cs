using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool emailConfirmed { get; set;}
        public bool isAdmin  {get; set;}
        public int ammountStars { get; set; }
        public int ammountLections { get; set; }

       // public Like Like { get; set; }
        public List<Lection> Lections  { get; set; }
        public List<Likes> Likes { get; set; }

        public User()
        {
            emailConfirmed = false;
            isAdmin = false;
        }

        public User(string username)
        {
            this.username = username;
            isAdmin = false;
        }

        public void setUserFromObj(User user)
        {
            firstname = user.firstname;
            lastname = user.lastname;
            email = user.lastname;
            username = user.lastname;
            password = user.password;
            emailConfirmed = user.emailConfirmed;
            isAdmin = user.isAdmin;
            ammountStars = user.ammountStars;
            ammountLections = user.ammountLections;
        }

    }
}
