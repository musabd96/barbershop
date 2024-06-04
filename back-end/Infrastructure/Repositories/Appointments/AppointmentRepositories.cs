﻿

using Domain.Models.Appointments;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Appointments
{
    public class AppointmentRepositories : IAppointmentRepositories
    {
        private readonly AppDbContext _appDbContext;
        public AppointmentRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken)
        {
            try
            {
                List<Appointment> allAppointments = await _appDbContext.Appointment.ToListAsync(cancellationToken);

                return allAppointments;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all appointments from the database", ex);
            }
        }

        public async Task<Appointment> GetAppointmentById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Appointment? wantedAppointment = await _appDbContext.Appointment
                    .FirstOrDefaultAsync(appointment => appointment.Id == id, cancellationToken);

                return wantedAppointment!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the appointment from the database", ex);
            }
        }


        public Task<Appointment> AddNewAppoinment(Appointment newAppointment, CancellationToken cancellationToken)
        {
            try
            {
                _appDbContext.Appointment.Add(newAppointment);
                _appDbContext.SaveChangesAsync(cancellationToken);
                return Task.FromResult(newAppointment);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the appointment to the database", ex);
            }
        }

        public async Task<Appointment> UpdateAppointment(Guid appointmentId, Guid CustomerId, Guid barberId, DateTime appointmentDate, string service, decimal price, bool isCancelled, CancellationToken cancellationToken)
        {
            Appointment appointmentToUpdate = await _appDbContext.Appointment.FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);

            if (appointmentToUpdate != null)
            {
                appointmentToUpdate.BarberId = barberId;
                appointmentToUpdate.AppointmentDate = appointmentDate;
                appointmentToUpdate.Service = service;
                appointmentToUpdate.Price = price;
                appointmentToUpdate.IsCancelled = isCancelled;

                _appDbContext.Appointment.Update(appointmentToUpdate);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return appointmentToUpdate;
            }

            return null!;
        }

    }
}
