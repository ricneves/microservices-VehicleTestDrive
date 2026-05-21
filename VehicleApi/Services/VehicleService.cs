using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Interfaces;
using VehicleApi.Models;

namespace VehicleApi.Services
{
    public class VehicleService : IVehicle
    {
        private ApiDbContext dbContext;

        public VehicleService()
        {
            dbContext = new ApiDbContext();
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicle = await dbContext.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                dbContext.Vehicles.Remove(vehicle);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var vehicles = await dbContext.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = await dbContext.Vehicles.FindAsync(id);
            return vehicle;
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            var existingVehicle = await dbContext.Vehicles.FindAsync(id);
            if (existingVehicle != null)
            {
                existingVehicle.Name = vehicle.Name;
                existingVehicle.Price = vehicle.Price;
                existingVehicle.ImageUrl = vehicle.ImageUrl;
                existingVehicle.Displacement = vehicle.Displacement;
                existingVehicle.MaxSpeed = vehicle.MaxSpeed;
                existingVehicle.Length = vehicle.Length;
                existingVehicle.Width = vehicle.Width;
                existingVehicle.Height = vehicle.Height;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

