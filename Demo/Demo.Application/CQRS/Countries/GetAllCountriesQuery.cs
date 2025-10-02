using AutoMapper;
using Demo.Application.Interfaces;
using MediatR;

namespace Demo.Application.CQRS.Countries
{
    public class GetAllCountriesQuery : IRequest<IEnumerable<CountryDTO>>
    { }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryDTO>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        public GetAllCountriesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;   
        }
        public async Task<IEnumerable<CountryDTO>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await uow.CountryRepository.GetAll();
            return mapper.Map<IEnumerable<CountryDTO>>(countries);
        }
    }
}
