using Demo.Domain;
using System.Collections.Generic;

namespace Demo.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<City> CreateCity(City city);
        Task<IEnumerable<City>> GetAllWithCountry();

        Task<City> GetByIdWithCountry(int id);
        bool AnyWithCountry(int countryId);

        IEnumerable<City> GetByCountry(int countryId);

        Task<IEnumerable<City>> GetByCountryId(int country);

        Task<City?> GetByName(string name);

        void Delete(IEnumerable<City> cities);


    }
}
