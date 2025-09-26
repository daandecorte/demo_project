using Demo.Domain;
using static Demo.Application.Interfaces.ICountryRepository;
namespace Demo.Application.Interfaces
{
    public interface ICountryService
    {
        Task<PagedResult<Country>> GetAll(string? nameFilter = null, SortBy? sortBy = null, int pageNumber = 1, int pageLength = 10);
        Country GetByName(string name);
    }
}
