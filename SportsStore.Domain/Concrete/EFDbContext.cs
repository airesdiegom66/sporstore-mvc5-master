using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        //To take advantage of the code-first feature, 
        //I need to create a class that is derived from System.Data.Entity.DbContext.
        //This class then automatically defines a property for each table in the database that I want to work with.
        //The name of the property specifies the table, and the type parameter of the DbSet result specifies the model type that the 
        //Entity Framework should use to represent rows in that table. In this case, the property name is Products and the type parameter 
        //is Product, meaning that the Entity Framework should use the Product model type to represent rows in the Products table.
        public DbSet<Product> Products { get; set; }
    }
}
