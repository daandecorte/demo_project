using Demo.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<PagedResult<Country>> GetAll(string? nameFilter = null, SortBy? sortBy = null, int pageNumber = 1, int pageLength = 10);

        public enum SortBy
        {
            ByNameAscending,
            ByNameDescending
        }
        Country GetByName(string name);
    }
    public enum sortBy
    {
        cityAscending,
        cityDescending
    }
}
