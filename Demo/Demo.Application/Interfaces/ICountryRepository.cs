using Demo.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<IEnumerable<Country>> GetAll(string? nameFilter = null, SortBy? sortBy = null);

        public enum SortBy
        {
            ByNameAscending,
            ByNameDescending
        }
        Task<Country?> GetByName(string name);
    }
    public enum sortBy
    {
        cityAscending,
        cityDescending
    }
}
