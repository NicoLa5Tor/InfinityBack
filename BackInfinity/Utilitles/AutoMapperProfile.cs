using AutoMapper;
using BackInfinity.Models;
using BackInfinity.Models.DTO;
using System.Globalization;

namespace BackInfinity.Utilitles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Services
            CreateMap<Service, ServiceDTO>().ForMember(destiny => destiny.Id,
                origin => origin.MapFrom(org => org.Id.ToString())).
                ForMember(destiny => destiny.Price,
                origin => origin.MapFrom(org => org.Price.ToString())).
                ForMember(destiny => destiny.HorMin,
                origin => origin.MapFrom(org => org.HorMin.ToString()));
            CreateMap<ServiceDTO, Service>().ForMember(destiny => destiny.Id,
                origin => origin.Ignore()).
                ForMember(destiny => destiny.Price,
                origin => origin.MapFrom(dest => int.Parse(dest.Price))).
                ForMember(destiny => destiny.HorMin,
                origin => origin.MapFrom(org => TimeSpan.Parse(org.HorMin)));
            #endregion
            #region Appointment
            _ = CreateMap<Appointment, AppointmentDTO>().
               ForMember(destiny => destiny.IdAppointment,
               origin => origin.MapFrom(org => org.IdAppointment.ToString())
               ).
               ForMember(destiny => destiny.IdService,
               origin => origin.MapFrom(org => org.IdService.ToString())).
               ForMember(destiny => destiny.NameService,
               origin => origin.MapFrom(org => org.IdServiceNavigation.NameService)).
               ForMember(destiny => destiny.Tel,
               origin => origin.MapFrom(org => org.Tel.ToString())).
               ForMember(destiny => destiny.HorCreate,
               origin => origin.MapFrom(org => org.HorCreate.ToString("dd/MM/yyyy H:mm"))).
               ForMember(destiny => destiny.HorAppoint,
               origin => origin.MapFrom(org => org.HorAppoint.ToString("dd/MM/yyyy H:mm")));
            CreateMap<AppointmentDTO, Appointment>().
                ForMember(destiny => destiny.IdService,
                origin => origin.MapFrom(org => int.Parse(org.IdService))).
                ForMember(destiny => destiny.IdAppointment,
                origin => origin.Ignore()).
                ForMember(destiny => destiny.Tel,
                origin => origin.MapFrom(org => long.Parse(org.Tel))).
                ForMember(destiny => destiny.HorCreate,
                origin => origin.Ignore()).
                ForMember(destiny => destiny.HorAppoint,
                origin => origin.MapFrom(org =>
                DateTime.ParseExact(org.HorAppoint, "dd/MM/yyyy H:mm", CultureInfo.InvariantCulture)));
            #endregion
            #region Access
            CreateMap<Access, AccessDTO>().ReverseMap();
            #endregion

        }

    }
}
