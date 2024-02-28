using Microsoft.EntityFrameworkCore;
using OracleMinimalAPI.Models;

namespace OracleMinimalAPI.Config
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}