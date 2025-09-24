using AutoMapper;
using Demo.Application.Interfaces;
using MediatR;

namespace Demo.Application.CQRS.Countries
{
    public class GetAllCountriesQuery : IRequest<PagedResult<CountryDTO>>
    {
        public int PageNr { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, PagedResult<CountryDTO>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        public GetAllCountriesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;   
        }
        public async Task<PagedResult<CountryDTO>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var pagedCountries = await uow.CountryRepository.GetAll(
                pageNumber: request.PageNr,
                pageLength: request.PageSize
            );
            return mapper.Map<PagedResult<CountryDTO>>(pagedCountries);
        }
    }
}
