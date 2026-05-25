using Azure.Identity;
using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;

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

            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using ServiceBusClient client = new(connectionString);
            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new("Hello world!");

            // send the message
            await sender.SendMessageAsync(message);
        }
        catch (Exception)
        {
            throw;
        }

    }
}
