using AutoMapper;
using Demo.Application.CQRS.Cities;
using Demo.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Countries
{
    public class GetAllCitiesByCountryQuery : IRequest<IEnumerable<CityDTO>>
    {
        public int Id { get; set; }
    }

    public class GetAllCitiesByCountryQueryHandler : IRequestHandler<GetAllCitiesByCountryQuery, IEnumerable<CityDTO>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public GetAllCitiesByCountryQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> Handle(GetAllCitiesByCountryQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CityDTO>>(await uow.CityRepository.GetByCountryId(request.Id));
        }
    }
}
