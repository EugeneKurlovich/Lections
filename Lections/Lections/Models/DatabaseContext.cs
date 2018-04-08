using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lections.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lection> Lections { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        {

        }
        public DatabaseContext()
        {

        }

    }
}
