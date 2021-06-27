using dotnet_webapi.Helpers;
using dotnet_webapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
        public DbSet<Event> Event { get; set; }

        public PagedList<Event> GetEvents(EventParameters eventParameters)
        {
            

            return PagedList<Event>.ToPageList( this.Event.OrderBy(on=> on.Name), eventParameters.PageNumber, eventParameters.PageSize);

        }




    }
}