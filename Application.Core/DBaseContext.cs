using Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Core
{
    public class DBaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Books> Books { get; set; }

        public DBaseContext(DbContextOptions<DBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
