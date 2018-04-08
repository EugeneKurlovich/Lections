using Lections.Models;
using Lections.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Services
{
    public class LectionService
    {
        UnitOfWork unitOfWork;

        public LectionService(DatabaseContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Lection> getAllLections()
        {
            return unitOfWork.Lections.GetAll();
        }

        public bool checkExistLectionName(string name)
        {
            foreach (Lection l in getAllLections())
            {
                if (l.name.Equals(name))
                    return false;
            }
            return true;
        }

        public void createUserLection(Lection lection)
        {
            unitOfWork.Lections.Create(lection);
        }

        public int getAmmountStars(int id)
        {
            return unitOfWork.Lections.Get(id).UserId;
        }

        public Lection getAuthorByLName(string lName)
        {
            foreach ( Lection u in unitOfWork.Lections.GetAll())
            {
                if (u.name.Equals(lName))
                {
                    return u;
                }
            }
            return null;
        }

        public void likeLection(int idLection, double nowStar)
        {
            Lection lection = unitOfWork.Lections.Get(idLection);
            lection.stars = nowStar;
            unitOfWork.Lections.Update(lection);
        }

        public Lection getLectionByName(string name)
        {
            foreach (Lection lect in getAllLections())
            {
                if (lect.name.Equals(name))
                {
                    return lect;
                }
            }
            return null;
        }

        public void updateUserLection (Lection lection)
        {
            unitOfWork.Lections.Update(lection);
        }

        public void deleteUserLection(Lection lection)
        {
            unitOfWork.Lections.Delete(lection.Id);
        }

        public void Save()
        {
            unitOfWork.Save();
        }

        public IEnumerable<Lection> getLectionsByUser(int id)
        {
            var lections = (from l in getAllLections() where l.UserId == id select l).ToList();
            return lections;
        }

    }
}
