using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using BackInfinity.Models.Appis;
using BackInfinity.Services.Contract;

namespace BackInfinity.Controllers
{
    [Route("Appi")]
    [ApiController]
    public class AppisController : ControllerBase
    {
        readonly IAppisService appisService;
        public AppisController(IAppisService appi)
        {
            appisService = appi;
        }
        [HttpPost("/Preference")]
        public async Task<IActionResult> MercadoPagoPreferences([FromBody] ModelMercadoPago model)
        {
            try
            {
                var preference = await appisService.Preference(model);
                if(preference is null ) return NotFound();
                return Ok(preference);

                
            }catch (Exception ex) { throw ex; }
        }
        [HttpGet("/GetPreference/{id}")]
        public async Task<IActionResult> MercadoPagoGetPreference(string id)
        {
            try
            {
                var response = await appisService.GetPreference(id);
                if(response is null) return NotFound();
                return Ok(response);

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
