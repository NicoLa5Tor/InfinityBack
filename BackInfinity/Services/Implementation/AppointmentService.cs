using BackInfinity.Models;
using BackInfinity.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackInfinity.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private DbinfinityContext _context;
        public AppointmentService(DbinfinityContext context)
        {
            this._context = context;        }
        public async Task<Appointment> AddAppointment(Appointment model)
        {
            try
            {
                _context.Appointments.Add(model);   
                await _context.SaveChangesAsync();  
                return model;

            }catch(Exception ex) { throw ex; }
        }

        public async Task<bool> DeleteAppointment(Appointment model)
        {
            try
            {
                _context.Appointments.Remove(model);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex) { throw ex; }
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            try
            {
                var appo = await _context.Appointments.Include(x => x.IdServiceNavigation).FirstOrDefaultAsync(x => x.IdAppointment == id);
                return appo;

            }catch(Exception ex) { throw ex; }
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            try
            {
                var listAppo = await _context.Appointments.ToListAsync();
                return listAppo;

            }catch(Exception ex) { throw ex; }
        }

        public async Task<int> ReturnTimeService(Appointment id)
        {
            try
            {
                var hours = await _context.Services.Where(x => x.Id == id.IdService).FirstOrDefaultAsync();
                if (hours.HorMin.HasValue)
                {
                    return hours.HorMin.Value.Hours;
                }
                else
                {
                    return 0;
                }

            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<bool> TimeMax(List<Appointment> list, Appointment model)
        {
            try
            {
                foreach (var item in list)
                {
                    if (item.HorAppoint.Month == model.HorAppoint.Month)
                    {
                        if (item.HorAppoint.Day == model.HorAppoint.Day)
                        {
                            if (item.HorAppoint.Hour < model.HorAppoint.Hour)
                            {
                                var hours = await ReturnTimeService(item);
                                int max = item.HorAppoint.Hour + hours;
                                if (model.HorAppoint.Hour < max)
                                {
                                    return true;
                                }
                            }
                            else if(item.HorAppoint.Hour == model.HorAppoint.Hour)
                            {
                                return true;
                            }else if (item.HorAppoint.Hour > model.HorAppoint.Hour)
                            {
                                var hours = await ReturnTimeService(item);
                                int max = item.HorAppoint.Hour - hours;
                                if (model.HorAppoint.Hour > max)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;

            }
            catch(Exception ex)
            { throw ex; }
        }

        public async Task<bool> TimeMin(List<Appointment> list, Appointment model)
        {
            try
            {
                int time = await ReturnTimeService(model);
                var timeModel = model.HorAppoint.Hour + time;
                foreach (var item in list)
                {
                    if (timeModel > item.HorAppoint.Hour)
                    {
                        return true;
                    }
                }
                return false;
            }catch(Exception ex) { throw ex; }
        }

        public async Task<bool> UpdateAppointment(Appointment model)
        {
            try
            {
                _context.Appointments.Update(model);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex) { throw ex; }
        }
       
    }
}
