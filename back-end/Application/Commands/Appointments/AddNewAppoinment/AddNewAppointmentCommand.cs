using Application.Dtos;
using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Appointments.AddNewAppoinment
{
    public class AddNewAppointmentCommand : IRequest<Appointment>
    {
        public AddNewAppointmentCommand(AppointmentDto newAppointment, string userName)
        {
            NewAppointment = newAppointment;
            UserName = userName;
        }

        public AppointmentDto NewAppointment { get; }
        public string UserName { get; }
    }
}
