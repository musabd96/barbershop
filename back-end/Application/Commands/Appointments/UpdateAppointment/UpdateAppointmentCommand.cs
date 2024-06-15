using Application.Dtos;
using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<Appointment>
    {
        public UpdateAppointmentCommand(AppointmentDto appointmentDto, Guid appointmentId, string username)
        {
            AppointmentDto = appointmentDto;
            AppointmentId = appointmentId;
            Username = username;
        }

        public AppointmentDto AppointmentDto { get; set; }
        public Guid AppointmentId { get; set; }
        public string Username { get; set; }
    }
}
