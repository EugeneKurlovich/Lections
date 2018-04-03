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

        public void updateAllProfile(User u)
        {
            unitOfWork.Users.Update(u);
        }

        public void updateProfile(User u)
        {
            unitOfWork.Users.Update(u);
        }

        public void getUserNameById(int id)
        {
            unitOfWork.Users.Get(id);
        }

        public void plusLection(User u)
        {
            unitOfWork.Users.Update(u);
        }

        public void minusLection(User u)
        {
            unitOfWork.Users.Update(u);
        }

        public void deleteProfile(User u)
        {
            unitOfWork.Users.Delete(u.Id);
        }

        public User getUserById(int id)
        {
            return unitOfWork.Users.Get(id);
        }

        public User getUserbyName(string uName)
        {
            foreach (User u in unitOfWork.Users.GetAll())
            {
                if (u.username.Equals(uName))
                    return u;
            }
            return null;
        }

        public void delAdm(User user)
        {
            user.isAdmin = false;
        }

        public void makeAdm(User user)
        {

            user.isAdmin = true;

        }

        public int getUserIdByName(string username)
        {
            foreach(User i in getAllUsers())
            {
                if(i.username.Equals(username))
                {
                    return i.Id;
                }
            }
            return 0;
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

        public bool isAdmin(string username)
        {
            foreach (User user in unitOfWork.Users.GetAll())
            {
                if (user.username.Equals(username) && user.isAdmin)
                {
                    return true;
                }
            }
            return false;
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
