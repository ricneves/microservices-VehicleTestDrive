using Azure.Identity;
using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomersApi.Services;

public class CustomerService : ICustomer
{
    private ApiDbContext dbContext;

    public CustomerService()
    {
        dbContext = new ApiDbContext();
    }

    public async Task AddCustomer(Customer customer)
    {
        var vehicleInDb = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
        if (vehicleInDb == null)
        {
            await dbContext.Vehicles.AddAsync(customer.Vehicle);
            await dbContext.SaveChangesAsync();
        }
        customer.Vehicle = null;
        await dbContext.Customers.AddAsync(customer);
        await dbContext.SaveChangesAsync();

        await SendMessageToServiceBus(customer);
    }

    private static async Task SendMessageToServiceBus(Customer customer)
    {
        try
        {
            string connectionString = "";
            string queueName = "azureorderqueue";

            string customerJson = JsonConvert.SerializeObject(customer);

            await using ServiceBusClient client = new(connectionString);

            ServiceBusSender sender = client.CreateSender(queueName);

            ServiceBusMessage message = new(customerJson);

            await sender.SendMessageAsync(message);
        }
        catch (Exception)
        {
            throw;
        }

    }
}
