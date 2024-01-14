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
            
        }

    }
}
