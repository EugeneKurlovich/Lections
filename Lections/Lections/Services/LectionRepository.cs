using Lections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Services
{
    public class LectionRepository : IRepository<Lection>
    {
        private DatabaseContext db;

        public LectionRepository(DatabaseContext context)
        {
            this.db = context;
        }

        public IEnumerable<Lection> GetAll()
        {
            return db.Lections;
        }

        public Lection Get(int id)
        {
            return db.Lections.Find(id);
        }

        public void Create(Lection lection)
        {
            db.Lections.Add(lection);
        }

        public void Update(Lection lection)
        {
            db.Entry(lection).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Lection lection = db.Lections.Find(id);
            if (lection != null)
                db.Lections.Remove(lection);
        }
    }
}
