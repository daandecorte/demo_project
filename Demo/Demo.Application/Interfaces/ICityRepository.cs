using Demo.Domain;
using System.Collections.Generic;

namespace Demo.Application.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        bool AnyWithEmployer(int countryId);

        IEnumerable<City> GetByEmployer(int countryId);

        void Delete(IEnumerable<City> cities);


    }
}
