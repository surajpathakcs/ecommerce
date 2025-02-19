using ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace ecom.DAO
{
    public class ApplicationDbContext:DbContext
    {   
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
    }   
}
