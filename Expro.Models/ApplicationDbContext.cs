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

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Gender> Genders { get; set; }
    }
}
