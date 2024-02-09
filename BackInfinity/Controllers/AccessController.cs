using AutoMapper;
using BackInfinity.Models;
using BackInfinity.Models.DTO;
using BackInfinity.Models.Encrypt;
using BackInfinity.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackInfinity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService accessService;
        private readonly IMapper mapper;
       
        public AccessController(IAccessService access,IMapper map)
        {
            this.accessService = access;
            this.mapper = map;
            
        }
        [HttpPost("Add")]
        public async Task<IActionResult> CreateAccess([FromBody] AccessDTO model)
        {
            try
            {
                model.Password = Encryption.Encrypt(model.Password);
                var create = await accessService.CreateAccess(mapper.Map<Access>(model));
                if (create is null) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(mapper.Map<AccessDTO>(create));

            }catch(Exception ex) { throw ex; }
        }
        [HttpDelete("Delete/{userName}")]
        public async Task<IActionResult> RemoveAccess(string userName)
        {
            try
            {
                var access = await accessService.InitAcces(userName);
                if (access is null) return NotFound(false);
                var delete = await accessService.RemoveAccess(access);
                if (!delete) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(true);

            }catch(Exception ex) { throw ex; }

        }

        [HttpGet("GetAccess/{userName}")]
        public async Task<IActionResult> GetAccess(string userName)
        {
            try
            {
                var get = await accessService.InitAcces(userName);
                if (get is null) return NotFound(false);
                return Ok(mapper.Map<AccessDTO>(get));

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
