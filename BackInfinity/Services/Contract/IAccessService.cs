using BackInfinity.Models;

namespace BackInfinity.Services.Contract
{
    public interface IAccessService
    {
        public Task<Access> CreateAccess(Access model);
        public Task<bool> RemoveAccess(Access model);
        public Task<Access> InitAcces(string nameUser);
    }
}
