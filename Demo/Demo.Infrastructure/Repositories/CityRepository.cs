using Demo.Application.Interfaces;
using Demo.Domain;
using Demo.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DemoContext context;

        public CityRepository(DemoContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<City>> GetAllWithCountry()
        {
            return await context.Cities
                .Include(p => p.Country)
                .ToListAsync();
        }

        public async Task<City> GetByIdWithCountry(int id)
        {
            return await context.Cities
                .Include(p => p.Country)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool AnyWithCountry(int countryId)
        {
            return context.Cities.Any(p => p.CountryId == countryId);
        }

        public IEnumerable<City> GetByCountry(int countryId)
        {
            return context.Cities.Where(p => p.CountryId == countryId);
        }

        public async Task<IEnumerable<City>> GetByCountryId(int country)
        {
            return await context.Cities
                .Where(p => p.CountryId == country)
                .Include(p => p.Country)
                .ToListAsync();
        }

        public void Delete(IEnumerable<City> cities)
        {
            context.Cities.RemoveRange(cities);
        }
    }
}
