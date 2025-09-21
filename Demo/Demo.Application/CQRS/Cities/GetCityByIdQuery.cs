using AutoMapper;
using Demo.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class GetCityByIdQuery : IRequest<CityDTO>
    {
        public int Id { get; set; }
    }

    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        public GetCityByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<CityDTO> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<CityDTO>(await uow.CityRepository.GetByIdWithCountry(request.Id));
        }
    }
}
