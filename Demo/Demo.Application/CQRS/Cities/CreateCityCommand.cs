using AutoMapper;
using Demo.Application.Interfaces;
using Demo.Domain;
using FluentValidation;
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

        public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
        {
            private IUnitofWork uow;

            public CreateCityCommandValidator(IUnitofWork uow)
            {
                this.uow = uow;
                RuleFor(c => c.City.Name).Must(name =>
                {
                    return name.Length > 0;
                }).WithMessage(c => $"You need to enter a city name.");

                RuleFor(c => c.City.Population).Must(population =>
                {
                    return population < 10000000000;
                }).WithMessage("Cannot delete the last city.");

                RuleFor(c => c.City.CountryId).MustAsync(async (countryId, cancellation) =>
                {
                    var country = await uow.CountryRepository.GetById(countryId);
                    return country != null ;
                }).WithMessage("The selected country does not exist.");
            }
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
