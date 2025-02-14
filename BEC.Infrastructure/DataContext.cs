using BEC.Domain.Admin;
using Microsoft.EntityFrameworkCore;

namespace BEC.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Page>().HasData(

                new Page { Id = 1, Title = "Home", Slug= "home", Body = "This is the Home Page"},
                new Page { Id = 2, Title = "About", Slug= "about", Body = "This is the About Page" },
                new Page { Id = 3, Title = "Services", Slug= "services", Body = "This is the Services Page" },
                new Page { Id = 4, Title = "Contact", Slug= "contact", Body = "This is the Contact Page" }

                );
        }
    }
}
