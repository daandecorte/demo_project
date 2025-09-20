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
        /*
        public async Task<PagedResult<Store>> GetAll(string? nameFilter = null, IStoresRepository.SortBy? sortBy = null, int pageNumber = 1, int pageLength = 10)
        {
            var result = new PagedResult<Store>();
            result.PageNumber = pageNumber;
            result.PageSize = pageLength;

            //Starting to build query taking into account the parameters given
            //As long as we work with IQueryable the actual query is not performed yet (deferred execution)
            IQueryable<Store> query = context.Stores;

            //Also fill in the total number of items already.
            result.TotalRecordCount = query.Count();

            //filtering first
            if (nameFilter != null)
                query = query.Where(s => s.Name.Contains(nameFilter));

            //fill in the filtered count (paging will not change that)
            result.FilteredRecordCount = query.Count();
            //now we can calculate the total number of pages (for the filtered set of results)
            result.TotalNumberOfPages = (int)Math.Ceiling((double)result.FilteredRecordCount / result.PageSize);

            //Then sorting (if required)
            switch (sortBy)
            {
                case SortBy.ByNameAscending:
                    query = query.OrderBy(s => s.Name);
                    break;
                case SortBy.ByNameDescending:
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
        */

        public Country GetByName(string name)
        {
            return context.Countries.FirstOrDefault(s => s.Name == name);
        }
            

    }
}
