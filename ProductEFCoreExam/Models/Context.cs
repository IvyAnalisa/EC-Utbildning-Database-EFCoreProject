using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace ProductEFCoreExam.Models
{
    internal class Context : DbContext
    {
        internal static object Category;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProductEFCoreExam;Trusted_Connection=True;");
             
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(x =>
                x.HasIndex(x => x.ProductId)
                .IsUnique());
            
        }
        public DbSet<Product>? products { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}
