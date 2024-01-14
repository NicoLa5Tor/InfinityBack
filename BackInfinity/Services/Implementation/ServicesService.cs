using BackInfinity.Models;
using BackInfinity.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackInfinity.Services.Implementation
{
    public class ServicesService : IServicesService
    {
        private DbinfinityContext  _context;
        public ServicesService(DbinfinityContext context)
        {
            this._context = context;
            
        }
        public async Task<Service> AddService(Service model)
        {
            try
            {
                _context.Services.Add(model);
                await _context.SaveChangesAsync();
                return model;


            }catch (Exception ex) { throw ex; }

        }

        public async Task<bool> DeleteService(Service model)
        {
            try
            {
                _context.Services.Remove(model);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex) { throw ex; }  
        }

        public async Task<Service> GetService(int id)
        {
            try
            {
                var opt = await _context.Services.Where(x => x.Id == id).FirstOrDefaultAsync();
                return opt;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Service>> ListServices()
        {
            try
            {
                var list = await _context.Services.ToListAsync();
                return list;

            }catch( Exception ex) { throw ex; }
        }

        public async Task<bool> UpdateService(Service model)
        {
            try
            {
                _context.Services.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex) { throw ex; }
        }
    }
}
