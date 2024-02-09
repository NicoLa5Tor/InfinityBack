using BackInfinity.Models.Appis;
using BackInfinity.Services.Contract;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System.Globalization;
using System.Text.Json;

namespace BackInfinity.Services.Implementation
{
    public class AppisService : IAppisService
    {
        public async Task<string> GetPreference(string id)
        {
            try
            {
                //url get api mercado pago
                string url = "https://api.mercadopago.com/checkout/preferences";
                //access token
                string accessToken = "TEST-1759104785506738-020116-4e1bf5f221b1e41876db739257ce3f0f-677479999";
                using (HttpClient client = new HttpClient()) { 
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    //solicitud get
                    HttpResponseMessage response = await client.GetAsync($"{url}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y mostrar el contenido de la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var preference = JsonSerializer.Deserialize<Preference>(responseBody);
                        return responseBody;
                    }
                    else
                    {
                        // Mostrar el código de error si la solicitud no fue exitosa
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return null;
                    }
                }

            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Preference> Preference(ModelMercadoPago model)
        {
            try
            {
                
                MercadoPagoConfig.AccessToken = "TEST-1759104785506738-020116-4e1bf5f221b1e41876db739257ce3f0f-677479999";
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
