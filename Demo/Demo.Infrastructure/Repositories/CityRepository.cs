using Demo.Application.Interfaces;
using Demo.Domain;
using Demo.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DemoContext context;

        public CityRepository(DemoContext context) : base(context)
        {
            this.context = context;
        }

        public bool AnyWithCountry(int countryId)
        {
            return context.Cities.Any(p => p.CountryId == countryId);
        }

        public IEnumerable<City> GetByCountry(int countryId)
        {
            return context.Cities.Where(p => p.CountryId == countryId);
        }

        public void Delete(IEnumerable<City> cities)
        {
            context.Cities.RemoveRange(cities);
        }
    }
}
