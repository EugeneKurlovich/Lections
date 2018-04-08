using Lections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Repository
{
    public class LikeRepository : IRepository<Likes>
    {
        private DatabaseContext db;

        public LikeRepository(DatabaseContext context)
        {
            this.db = context;
        }

        public IEnumerable<Likes> GetAll()
        {
            return db.Likes;
        }

        public Likes Get(int id)
        {
            return db.Likes.Find(id);
        }

        public void Create(Likes like)
        {
            db.Likes.Add(like);
        }

        public void Update(Likes like)
        {
            db.Entry(like).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Likes like = db.Likes.Find(id);
            if (like != null)
                db.Likes.Remove(like);
        }
    }
}
