using dotnet_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Business> Business { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<User> User { get; set; }

       


    }
}