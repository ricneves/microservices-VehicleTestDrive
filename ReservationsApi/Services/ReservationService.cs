using Microsoft.EntityFrameworkCore;
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
        // TODO: later
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
