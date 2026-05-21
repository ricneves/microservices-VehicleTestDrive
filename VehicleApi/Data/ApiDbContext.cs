using Microsoft.EntityFrameworkCore;
using VehicleApi.Models;

namespace VehicleApi.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VehicleApiDb;");
    }
}
