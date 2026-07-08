using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationsApi.Services;

public class ReservationService : IReservation
{
    private ApiDbContext dbContext;
    public ReservationService()
    {
        dbContext = new ApiDbContext();
    }
    public async Task<List<Reservation>> GetReservations()
    {
        string connectionString = "";
        string queueName = "azureorderqueue";

        await using ServiceBusClient client = new(connectionString);

        ServiceBusReceiver receiver = client.CreateReceiver(queueName);

        IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);

        if (receivedMessages == null)
            return null;

        foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
        {
            string body = receivedMessage.Body.ToString();
            var messageCreated = JsonConvert.DeserializeObject<Reservation>(body);

            await dbContext.Reservations.AddAsync(messageCreated);
            await dbContext.SaveChangesAsync();

            await receiver.CompleteMessageAsync(receivedMessage);
        }

        return await dbContext.Reservations.ToListAsync();
    }

    public async Task UpdateMailStatus(int id)
    {
        var reservation = await dbContext.Reservations.FindAsync(id);
        if (reservation != null && !reservation.IsMailSent)
        {
            string smtpEmail = "email@outlook.com";
            var smtpClient = new SmtpClient("smtp.live.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(smtpEmail, "password"),
                EnableSsl = true
            };
            smtpClient.Send(smtpEmail, reservation.Email, "Vehicle test drive", "Your test drive is reserved");
            reservation.IsMailSent = true;
            await dbContext.SaveChangesAsync();
        }
    }
}
