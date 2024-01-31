using BackInfinity.Models.Appis;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace BackInfinity.Services.Contract
{
    public interface IAppisService
    {
        public Task<Preference> Preference(ModelMercadoPago model);

    }
}
