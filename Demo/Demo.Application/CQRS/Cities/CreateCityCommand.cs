using AutoMapper;
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

            //Country? country = await uow.CountryRepository.GetByName(request.City.Country);
            Country? country = await uow.CountryRepository.GetById(request.City.CountryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            mapper.Map(request.City, city);
            city.Country = country;

            var newCity = await uow.CityRepository.CreateCity(city);
            await uow.Commit();
            return mapper.Map<CityDTO>(city);
        }
    }
}
