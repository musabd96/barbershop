using Application.Dtos;
using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Appointments.AddNewAppoinment
{
    public class AddNewAppointmentCommand : IRequest<Appointment>
    {
        public AddNewAppointmentCommand(AppointmentDto newAppointment)
        {
            NewAppointment = newAppointment;
        }

        public AppointmentDto NewAppointment { get; }
    }
}
