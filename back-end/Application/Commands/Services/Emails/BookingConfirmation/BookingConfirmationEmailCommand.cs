using Application.Dtos;
using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Services.Emails.BookingConfirmation
{
    public class BookingConfirmationEmailCommand : IRequest<Appointment>
    {
        public BookingConfirmationEmailCommand(AppointmentDto appointmentDto, string userName)
        {
            AppointmentDto = appointmentDto;
            UserName = userName;
        }
        public string UserName { get; set; }
        public AppointmentDto AppointmentDto { get; set; }
    }
}
