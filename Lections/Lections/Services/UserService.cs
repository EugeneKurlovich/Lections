using Lections.Models;
using Lections.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Services
{
    public class UserService 
    {
        UnitOfWork unitOfWork;


        public UserService(DatabaseContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public bool checkExist(string username, string email)
        {
            if (Array.Exists(unitOfWork.Users.GetAll().ToArray(), eU => eU.username.Equals(username)
            || eU.email.Equals(email)))
                return false;
            else
                return true;
        }   

        public IEnumerable<User> getAllUsers()
        {
            return unitOfWork.Users.GetAll();
        }

        public void confirmEmail(string username)
        {
            var users = from u in unitOfWork.Users.GetAll()
                        where u.username.Equals(username)
                        select u;
            foreach (User user in users)
            {
                user.emailConfirmed = true;
            }
        }

        public bool checkRegisteredUser(string log, string pass)
        {
            var users = from u in unitOfWork.Users.GetAll() where u.username.Equals(log) && u.password.Equals(pass) select u;
            foreach (User u in users)
            {
                if (u.emailConfirmed)
                {
                    return true;
                }
            }
            return false;
        }

        public void Save()
        {
            unitOfWork.Save();
        }

        public void createUser(User user)
        {
            unitOfWork.Users.Create(user);
        }
    }
}
