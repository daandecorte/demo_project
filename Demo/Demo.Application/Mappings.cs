using AutoMapper;
using Demo.Application.CQRS.Cities;
using Demo.Application.CQRS.Countries;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application
{
    internal class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CreateCityDTO, City>();
            CreateMap<City, CityDTO>()
                .ForMember(city => city.Country, 
                            opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<Country, CountryDTO>();

            CreateMap<City, UpdateCityDTO>();

            CreateMap<UpdateCityDTO, City>();

            // Add this for PagedResult<T>
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>))
                .ConvertUsing(typeof(PagedResultConverter<,>));
        }
    }
    
}
