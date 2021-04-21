using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchAppAPI.Models;

namespace SchAppAPI.Contexts
{
    public class SchoolDbContext  : IdentityDbContext<User>
    {
        //public DbSet<Teacher> Teachers { get; set; }

        public SchoolDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
base.OnModelCreating(builder);
        }

    }
}
