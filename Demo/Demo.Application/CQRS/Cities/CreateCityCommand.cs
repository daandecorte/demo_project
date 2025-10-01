using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Interfaces;
using Demo.Domain;
using MediatR;

namespace Demo.Application.CQRS.Cities
{
    public class CreateCityCommand : IRequest<CityDTO>
    {
        public CreateCityDTO City { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public CreateCityCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<CityDTO> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new City();
            mapper.Map(request.City, city);
            city = await uow.CityRepository.CreateCity(city);
            await uow.Commit();
            return mapper.Map<CityDTO>(city);
        }
    }
}
