using Microsoft.EntityFrameworkCore;
using WebAtrioApp.Core.Entities;

namespace WebAtrioApp.Infrastructure.Data
{
    public class WebAtrioDbContext(DbContextOptions<WebAtrioDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Job> Jobs { get; set; }



    }
}
