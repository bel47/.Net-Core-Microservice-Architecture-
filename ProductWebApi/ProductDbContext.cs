using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductWebApi.Model;

namespace ProductWebApi
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
            try
            {
                //To create DB if not exist
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create(); // create database if not connect
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables(); // create table if not table exist
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       public DbSet<Product> Products { get; set; }

    }
}
