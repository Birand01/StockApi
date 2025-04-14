using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
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
        }

        public DbSet<Stock> Stocks{get;set;}
        public DbSet<Comment> Comments{ get; set; }
    }
}