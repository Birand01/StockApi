using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        :base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
             modelBuilder.Entity<Comment>()
            .HasOne(c => c.Stock)
            .WithMany(s => s.Comments)
            .HasForeignKey(c => c.StockId)
            .OnDelete(DeleteBehavior.SetNull); 

            List<IdentityRole> roles = new List<IdentityRole>
{
    new IdentityRole
    {
        Id = "a1111111-aaaa-1111-aaaa-111111111111", // static GUID
        Name = "Admin",
        NormalizedName = "ADMIN"
    },
    new IdentityRole
    {
        Id = "b2222222-bbbb-2222-bbbb-222222222222",
        Name = "User",
        NormalizedName = "USER"
    }
};
modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        public DbSet<Stock> Stocks{get;set;}
        public DbSet<Comment> Comments{ get; set; }
    }
}