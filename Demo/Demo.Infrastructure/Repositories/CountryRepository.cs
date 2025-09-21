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
        public async Task<PagedResult<Country>> GetAll(string? nameFilter = null, ICountryRepository.SortBy? sortBy = null, int pageNumber = 1, int pageLength = 10)
        {
            var result = new PagedResult<Country>();
            result.PageNumber = pageNumber;
            result.PageSize = pageLength;

            IQueryable<Country> query = context.Countries;

            result.TotalRecordCount = query.Count();

            if (nameFilter != null)
                query = query.Where(s => s.Name.Contains(nameFilter));

            result.FilteredRecordCount = query.Count();
            result.TotalNumberOfPages = (int)Math.Ceiling((double)result.FilteredRecordCount / result.PageSize);

            switch (sortBy)
            {
                case ICountryRepository.SortBy.ByNameAscending:
                    query = query.OrderBy(s => s.Name);
                    break;
                case ICountryRepository.SortBy.ByNameDescending:
                    query = query.OrderByDescending(s => s.Name);
                    break;
                default:
                    break;
            }
            //paging must be the last step !
            query = query.Skip((pageNumber - 1) * pageLength).Take(pageLength);

            //here the actual query for filtered stores with paging is performed on the DB !
            result.Data = await query.ToListAsync();

            return result;
        }

        public Country GetByName(string name)
        {
            return context.Countries.FirstOrDefault(s => s.Name == name);
        }
            

    }
}
