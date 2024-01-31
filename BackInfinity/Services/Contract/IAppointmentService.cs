using BackInfinity.Models;

namespace BackInfinity.Services.Contract
{
    public interface IAppointmentService
    {
        public Task<Appointment> GetAppointment(int id);
        public Task<int> ReturnTimeService(Appointment id);
        public Task<List<Appointment>> GetAppointments();
        public Task<Appointment> AddAppointment(Appointment model);
        public Task<bool> UpdateAppointment(Appointment model);
        public Task<bool> DeleteAppointment(Appointment model);
        public Task<bool> TimeMax(List<Appointment> list, Appointment model);
        public Task<bool> TimeMin(List<Appointment> list, Appointment model);
    }
}
