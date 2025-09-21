using Demo.Application.Interfaces;
using Demo.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DemoContext ctxt;
        private readonly ICityRepository cityRepository;
        private readonly ICountryRepository countryRepository;

        public UnitofWork(DemoContext ctxt, ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            this.ctxt = ctxt;
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
        }

        public ICityRepository CityRepository => cityRepository;

        public ICountryRepository CountryRepository => countryRepository;

        public async Task Commit()
        {
            await ctxt.SaveChangesAsync();
        }
    }
}
