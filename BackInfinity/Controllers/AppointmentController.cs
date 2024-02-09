using AutoMapper;
using BackInfinity.Models;
using BackInfinity.Models.DTO;
using BackInfinity.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;

namespace BackInfinity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentService _appointmentService;
        private IMapper _mapper;

        public AppointmentController(IAppointmentService service, IMapper map)
        {
            this._appointmentService = service;
            this._mapper = map;

        }
        
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var get = await _appointmentService.GetAppointment(id);
                if (get is null) return NotFound(false);
                return Ok(_mapper.Map<AppointmentDTO>(get));

            }
            catch (Exception ex) { throw ex; }

        }
        [Authorize]
        [HttpPost("Add")] 
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentDTO model)
        {
            try
            {
                var transfrom = _mapper.Map<Appointment>(model);
                var existTo = await _appointmentService.GetAppointments();
                var timeMax = await _appointmentService.TimeMax(existTo, transfrom);
                if (timeMax) return NotFound(false);
                var timeMin = await _appointmentService.TimeMin(existTo, transfrom);
                if(timeMin) return NotFound(false);
                var create = await _appointmentService.AddAppointment(transfrom);
                if (create is null) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(_mapper.Map<AppointmentDTO>(create));
            }
            catch (Exception ex) { throw ex; }
        }
        
        [HttpGet("List")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var list = await _appointmentService.GetAppointments();
                if (list.Count < 1) return NotFound(false);
                return Ok(_mapper.Map<List<AppointmentDTO>>(list));

            }
            catch (Exception ex) { throw ex; }

        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var appo = await _appointmentService.GetAppointment(id);
                if (appo is null) return NotFound(false);
                var delete = await _appointmentService.DeleteAppointment(appo);
                if (!delete) return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(delete);

            }
            catch (Exception ex) { throw ex; }
        }

    }

}
    
