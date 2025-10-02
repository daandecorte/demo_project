using Demo.Application;
using Demo.Application.Interfaces;
using Demo.Domain;
using Demo.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DemoContext context;

        public CountryRepository(DemoContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Country>> GetAll(string? nameFilter = null, ICountryRepository.SortBy? sortBy = null)
        {
            //return await context.Countries
            //    .ToListAsync();

            IQueryable<Country> query = context.Countries;

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(c => c.Name.Contains(nameFilter));
            }

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    //case ICountryRepository.SortBy.ByNameAscending:
                    //    query = query.OrderBy(c => c.Name);
                    //    break;
                    case ICountryRepository.SortBy.ByNameDescending:
                        query = query.OrderByDescending(c => c.Name);
                        break;
                    default:
                        query = query.OrderBy(c => c.Name);
                        break;
                }
            }

            return await query.ToListAsync();
        }

        public Task<Country?> GetByName(string name)
        {
            return context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
