using Domain.Models.Appointments;
using Infrastructure.Database;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Appointments
{
    public class AppointmentRepositories : IAppointmentRepositories
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;

        public AppointmentRepositories(AppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<List<Appointment>> GetAllAppointments(string userName, CancellationToken cancellationToken)
        {
            try
            {
                var user = _appDbContext.User.FirstOrDefault(u => u.Username == userName);
                var customer = _appDbContext.UserCustomers.FirstOrDefault(uc => uc.UserId == user!.Id);

                if (customer != null)
                {
                    List<Appointment> allAppointments = await _appDbContext.Appointments.Where(a => a.CustomerId == customer.CustomerId).ToListAsync();

                    return allAppointments;
                }

                var barber = _appDbContext.UserBarbers.FirstOrDefault(ub => ub.UserId == user!.Id);

                List<Appointment> allAppointment = await _appDbContext.Appointments.Where(a => a.BarberId == barber!.BarberId).ToListAsync();

                return allAppointment;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all appointments from the database", ex);
            }
        }

        public async Task<Appointment> GetAppointmentById(Guid id, string userName, CancellationToken cancellationToken)
        {
            try
            {
                var user = _appDbContext.User.FirstOrDefault(u => u.Username == userName);
                var customer = _appDbContext.UserCustomers.FirstOrDefault(uc => uc.UserId == user!.Id);

                //var appointmentCustomer = _appDbContext.AppointmentCustomers.FirstOrDefault(ac => ac.CustomerId == customer.CustomerId && ac.AppointmentId == id);

                //if (appointmentCustomer != null)
                //{
                Appointment? wantedAppointment = await _appDbContext.Appointments
                .FirstOrDefaultAsync(appointment => appointment.Id == id, cancellationToken);

                return wantedAppointment!;
                //}

                //return null!;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the appointment from the database", ex);
            }
        }


        public async Task<Appointment> AddNewAppoinment(Appointment newAppointment, string userName, CancellationToken cancellationToken)
        {
            try
            {
                var user = _appDbContext.User.FirstOrDefault(u => u.Username == userName);
                var customer = _appDbContext.UserCustomers.FirstOrDefault(uc => uc.UserId == user!.Id);

                newAppointment.CustomerId = customer!.CustomerId;
                _appDbContext.Appointments.Add(newAppointment);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                // Send booking confirmation email delete this when front end finished
                await _emailService.SendBookingConfirmed(user!.Username, newAppointment, cancellationToken);

                return await Task.FromResult(newAppointment);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the appointment to the database", ex);
            }
        }

        public async Task<Appointment> UpdateAppointment(Guid appointmentId, string userName, Guid barberId, DateTime appointmentDate, string service, decimal price, bool isCancelled, CancellationToken cancellationToken)
        {
            try
            {
                var user = _appDbContext.User.FirstOrDefault(u => u.Username == userName);
                var customer = _appDbContext.UserCustomers.FirstOrDefault(uc => uc.UserId == user!.Id);

                //var appointmentCustomer = _appDbContext.AppointmentCustomers.FirstOrDefault(ac => ac.CustomerId == customer.CustomerId && ac.AppointmentId == appointmentId);

                //if (appointmentCustomer != null)
                //{
                Appointment appointmentToUpdate = await _appDbContext.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);

                appointmentToUpdate!.BarberId = barberId;
                appointmentToUpdate.AppointmentDate = appointmentDate;
                appointmentToUpdate.Service = service;
                appointmentToUpdate.Price = price;
                appointmentToUpdate.IsCanceled = isCancelled;

                _appDbContext.Appointments.Update(appointmentToUpdate);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return appointmentToUpdate;
                //}
                //return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the appointment to the database", ex);
            }
        }

        public async Task<Appointment> DeleteAppointment(Guid appointmentId, string userName, CancellationToken cancellationToken)
        {
            try
            {
                var user = _appDbContext.User.FirstOrDefault(u => u.Username == userName);
                var customer = _appDbContext.UserCustomers.FirstOrDefault(uc => uc.UserId == user!.Id);

                //var appointmentCustomer = _appDbContext.AppointmentCustomers.FirstOrDefault(ac => ac.CustomerId == customer.CustomerId && ac.AppointmentId == appointmentId);

                //if (appointmentCustomer != null)
                //{
                Appointment appointmentToUpdate = await _appDbContext.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);

                _appDbContext.Appointments.Remove(appointmentToUpdate);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return appointmentToUpdate;

                //}

                //return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the appointment to the database", ex);
            }
        }
    }
}
