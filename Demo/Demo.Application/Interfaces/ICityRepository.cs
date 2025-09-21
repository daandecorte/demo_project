using Demo.Domain;
using System.Collections.Generic;

namespace Demo.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        bool AnyWithCountry(int countryId);

        IEnumerable<City> GetByCountry(int countryId);

        void Delete(IEnumerable<City> cities);


    }
}
