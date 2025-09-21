using AutoMapper;
using Demo.Application.CQRS.Cities;
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
            CreateMap<City, CityDTO>();
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}
