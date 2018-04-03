using Lections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Services
{
    public class UnitOfWork : IDisposable
    {
        private DatabaseContext db = new DatabaseContext();
        private UserRepository userRepository;
        private LectionRepository lectionRepository;

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public LectionRepository Lections
        {
            get
            {
                if (lectionRepository == null)
                    lectionRepository = new LectionRepository(db);
                return lectionRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
