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
    public class GetAllCitiesQuery : IRequest<IEnumerable<CityDTO>>
    {
        public int PageNr { get; set; }
        public int PageSize { get; set; }
    }

    public class  GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<CityDTO>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public GetAllCitiesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CityDTO>>(await uow.CityRepository.GetAll());
        }
    }
}
