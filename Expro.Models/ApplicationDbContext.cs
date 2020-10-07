using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Expro.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLawArea>()
                .HasKey(bc => new { bc.UserID, bc.LawAreaID });
            modelBuilder.Entity<UserLawArea>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserLawAreas)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<UserLawArea>()
                .HasOne(bc => bc.LawArea)
                .WithMany(c => c.UserLawAreas)
                .HasForeignKey(bc => bc.LawAreaID);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<LawArea> LawAreas { get; set; }
    }
}
