using Demo.Application.Interfaces;
using Demo.Domain;

namespace Demo.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitofWork uow;
        public CountryService(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<PagedResult<Country>> GetAll(string? nameFilter = null, ICountryRepository.SortBy? sortBy = null, int pageNumber = 1, int pageLength = 10)
        {
            return await uow.CountryRepository.GetAll(nameFilter, sortBy, pageNumber, pageLength);
        }

        public Country GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
