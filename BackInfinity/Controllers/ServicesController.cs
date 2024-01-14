using AutoMapper;
using BackInfinity.Models;
using BackInfinity.Models.DTO;
using BackInfinity.Services.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackInfinity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
      private IMapper _mapper;
      private readonly IServicesService _service;
        public ServicesController(IMapper mapper, IServicesService service)
        {
            this._mapper = mapper;
            this._service = service;
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            try
            {
                var get = await _service.GetService(id);
                if (get is null) return NotFound();
                return Ok(_mapper.Map<ServiceDTO>(get));

            }catch(Exception ex) { throw ex; }
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var list = await _service.ListServices();
                if(list.Count <= 0) return NotFound();
                return Ok(_mapper.Map<List<ServiceDTO>>(list));
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Post")]
        public async Task<IActionResult> AddService([FromBody] ServiceDTO model)
        {
            try
            {
                var map = _mapper.Map<Service>(model);
                var add = await _service.AddService(map);
                if (add is null) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(_mapper.Map<ServiceDTO>(add));

            }catch(Exception ex) { throw ex; }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ServiceDTO model, int id)
        {
            try
            {
                var serviceTrue = await _service.GetService(id);
                if (serviceTrue is null) return NotFound();
                var update = _mapper.Map<Service>(model);
                serviceTrue.Price = update.Price;   
                serviceTrue.HorMin = update.HorMin;
                serviceTrue.NameService = update.NameService;   
                serviceTrue.Description1 = update.Description1; 
                serviceTrue.Description2 = update.Description2; 
                serviceTrue.Image1 = update.Image1;
                serviceTrue.Image2 = update.Image2;
                serviceTrue.Image3 = update.Image3;
                var up = await _service.UpdateService(serviceTrue);
                if (!up) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(_mapper.Map<ServiceDTO>(serviceTrue));
            }catch(Exception ex) { throw ex; }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var serviceTrue = await _service.GetService(id);
                if(serviceTrue is null) return NotFound();
                var delete = await _service.DeleteService(serviceTrue);
                if (!delete) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(delete);

            }catch(Exception ex) { throw ex; }
        }
    }
}
