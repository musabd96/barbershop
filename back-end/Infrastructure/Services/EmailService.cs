using Infrastructure.Database;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Appointments;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _appDbContext;

        public EmailService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<string> SendBookingConfirmed(string userName, Appointment newAppointment, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (newAppointment == null)
                throw new ArgumentNullException(nameof(newAppointment));

            var customerInfo = _appDbContext.Customers.FirstOrDefault(ci => ci.Id == newAppointment.CustomerId);

            if (customerInfo == null)
                throw new ArgumentException("Customer information not found.", nameof(newAppointment));

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("julio.pacocha@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("julio.pacocha@ethereal.email"));
            email.Subject = "Barbershop Appointment is Confirmed";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text =
                        $@"
                <html>
                <body>
                    <h2>Your Appointment is Confirmed</h2>
                    <p>Dear {customerInfo.FirstName} {customerInfo.LastName},</p>
                    <p>Thank you for booking your appointment with us! Here are the details of your appointment:</p>
                    <ul>
                        <li><strong>Date:</strong> {newAppointment.AppointmentDate:dddd, MMMM d, yyyy}</li>
                        <li><strong>Time:</strong> {newAppointment.AppointmentDate:hh:mm tt}</li>
                        <li><strong>Service:</strong> {newAppointment.Service}</li>
                        <li><strong>Price:</strong> {newAppointment.Price} SEK</li>
                    </ul>
                    <p>Please note that you can cancel or reschedule your booking up to 2 hours before the appointment time.</p>
                    <p>We look forward to seeing you soon!</p>
                    <p>Best regards,</p>
                    <p>The Barber Shop Team</p>
                </body>
                </html>"
            };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls, cancellationToken);

                // Authenticate with Mailgun SMTP server
                await smtp.AuthenticateAsync("julio.pacocha@ethereal.email", "qNkp9cCzg7qG5ebUJF", cancellationToken);

                // Send email
                await smtp.SendAsync(email, cancellationToken);

                return "Send Booking Confirmed was successful";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to send booking confirmation email.", ex);
            }
            finally
            {
                // Disconnect from SMTP server
                smtp.Disconnect(true);
            }
        }

    }
}
