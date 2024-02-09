using BackInfinity.Models;
using BackInfinity.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackInfinity.Services.Implementation
{
    public class AccessService : IAccessService
    {
        private DbinfinityContext _context;
        public AccessService(DbinfinityContext context)
        {

            _context = context;

        }
        public async Task<Access> CreateAccess(Access model)
        {
            try
            {
                 _context.Accesses.Add(model);
                await _context.SaveChangesAsync();
                return model;
                
            }catch (Exception ex) { throw ex; }
        }

        public async Task<Access> InitAcces(string nameUser)
        {
            try
            {
                var search = await _context.Accesses.Where(x => x.UserName == nameUser).FirstOrDefaultAsync();
                return search;
            } catch (Exception ex) { throw ex; } 
        }

        public async Task<bool> RemoveAccess(Access model)
        {
            try
            {
                _context.Accesses.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex) { throw ex; }
        }
    }
}
