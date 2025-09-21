using Demo.Domain;
using System.Collections.Generic;

namespace Demo.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IEnumerable<City>> GetAllWithCountry();

        Task<City> GetByIdWithCountry(int id);
        bool AnyWithCountry(int countryId);

        IEnumerable<City> GetByCountry(int countryId);

        Task<IEnumerable<City>> GetByCountryId(int country);

        void Delete(IEnumerable<City> cities);


    }
}
