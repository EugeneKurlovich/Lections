using Lections.Models;
using Lections.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Services
{
    public class LikeService
    {
        UnitOfWork unitOfWork;

        public LikeService(DatabaseContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public bool checkExistLike(int uId, int lId)
        {
            foreach (Likes l in unitOfWork.Like.GetAll())
            {
                if (l.LectionId.Equals(lId) && l.UserId.Equals(uId))
                {
                    return true;
                }
            }
            return false;
        }

        public Likes getLikeById(int uId, int lId)
        {
            foreach(Likes l in unitOfWork.Like.GetAll())
            {
                if (l.LectionId.Equals(lId) && l.UserId.Equals(uId))
                {
                    return l;
                }
            }
            return null;
        }

        public void deleteUserLikes(int id)
        {
            foreach (Likes l in unitOfWork.Like.GetAll())
            {
                if (l.UserId.Equals(id))
                {
                    unitOfWork.Like.Delete(l.Id);
                }
            }
        }

        public void updateLike(Likes like)
        {
            unitOfWork.Like.Update(like);
        }

        public double getNowRating(int id)
        {
            double rate = 0;
            int counter = 0;
            foreach(Likes l in getAll())
                {
                if (l.LectionId.Equals(id))
                {
                    rate += l.userStar;
                    counter++;
                }
            }
            double res = rate / counter;
            return res;
        }

        public IEnumerable<Likes> getAll()
        {
            return unitOfWork.Like.GetAll();
        }

        public void Save()
        {
            unitOfWork.Save();
        }

        public void addLike(Likes like)
        {
            unitOfWork.Like.Create(like);
        }
    }
}
