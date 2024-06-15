

using Domain.Models.Appointments;
using Infrastructure.Services;
using MediatR;

namespace Application.Commands.Services.Emails.BookingConfirmation
{
    public class BookingConfirmationEmailCommandHandler : IRequestHandler<BookingConfirmationEmailCommand, Appointment>
    {
        private readonly IEmailService _emailService;

        public BookingConfirmationEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Appointment> Handle(BookingConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            Appointment newAppointment = new Appointment()
            {
                Id = request.AppointmentDto.Id,
                CustomerId = request.AppointmentDto.CustomerId,
                BarberId = request.AppointmentDto.CustomerId,
                AppointmentDate = request.AppointmentDto.AppointmentDate,
                Service = request.AppointmentDto.Service,
                Price = request.AppointmentDto.Price,
                IsCancelled = request.AppointmentDto.IsCancelled,
            };

            await _emailService.SendBookingConfirmed(request.UserName, newAppointment, cancellationToken);

            return newAppointment;
        }
    }
}
