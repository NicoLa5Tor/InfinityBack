using BackInfinity.Models;

namespace BackInfinity.Services.Contract
    
{
    public interface IServicesService
    {
        public Task<List<Service>> ListServices();
        public Task<Service> GetService(int id);
        public Task<Service> AddService(Service model);
        public Task<bool> DeleteService(Service model);
        public Task<bool> UpdateService(Service model);
    }
}
