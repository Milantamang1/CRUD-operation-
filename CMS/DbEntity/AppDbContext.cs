using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CMS.DbEntity
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogCategory> BlogCategory { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    const string connectionString = "Server=Milan-Desktop\\SQLEXPRESS;Database=Employee;user=sa;password=abc123##;connect timeout=500;TrustServerCertificate=true"; optionsBuilder.UseSqlServer(connectionString);
        //}
    }
}
