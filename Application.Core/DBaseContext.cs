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
        public DbSet<DelayedBooks> DelayedBooks { get; set; }

        public DBaseContext(DbContextOptions<DBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().HasKey(x => x.Bks_ID);
            modelBuilder.Entity<Books>()
                .HasOne(x => x.User)
                .WithMany(x => x.Books)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserModel>().HasKey(x => x.Usr_ID);
            modelBuilder.Entity<UserModel>()
                .HasMany(x => x.Books)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserModel>()
                .HasMany(x => x.DelayedBooks)
                .WithOne(x => x.User);

            modelBuilder.Entity<DelayedBooks>()
                .HasOne(x => x.User)
                .WithMany(x => x.DelayedBooks);
        }
    }
}
