using BackInfinity.Models.Custom;
using BackInfinity.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackInfinity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorizationService service;
      
        public LoginController(IAuthorizationService authorizationService)
        {
            this.service = authorizationService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(
    [FromBody]
        AuthorizationRequest aut
        )
        {
            var resultAut = await service.ReturnToken(aut);
            if (resultAut is null) return Unauthorized();
            return Ok(resultAut);

        }
    }
}
