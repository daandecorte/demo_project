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
            CreateMap<City, CityDTO>()
                .ForMember(city => city.Country, 
                            opt => opt.MapFrom(src => src.Country.Name));
            //CreateMap<City, CityDTO>().ReverseMap();

            CreateMap<Country, CountryDTO>();
            //CreateMap<Country, CountryDTO>().ReverseMap();
        }
    }
}
