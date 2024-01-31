using BackInfinity.Models.Appis;
using BackInfinity.Services.Contract;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System.Globalization;

namespace BackInfinity.Services.Implementation
{
    public class AppisService : IAppisService
    {
        public async Task<Preference> Preference(ModelMercadoPago model)
        {
            try
            {
                
                MercadoPagoConfig.AccessToken = "TEST-7656813211249983-012917-89f4e7a5868aebdefc3fd418ddc7df9c-677479999";
                var request = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Title = model.title,
            Quantity = model.quantity,
            CurrencyId = model.currencyId,
            UnitPrice = model.unitPrice,
            PictureUrl = model.pictureUrl,
            EventDate = DateTime.ParseExact(model.eventDate,"dd/MM/yyyy",CultureInfo.InvariantCulture),
            Description = model.description,
        },
    },
                };

                // Crea la preferencia usando el client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                if (preference is null) return null;
                return preference;

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
