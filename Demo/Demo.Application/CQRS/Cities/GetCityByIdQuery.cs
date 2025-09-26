using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Interfaces;
using MediatR;

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
            var city = await uow.CityRepository.GetByIdWithCountry(request.Id);

            if (city == null)
                throw new CityNotFoundException($"City with id {request.Id} not found.");

            return mapper.Map<CityDTO>(city);
        }
    }
}
