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
        private readonly ICityRepository cityRepo;
        private readonly ICountryRepository countryRepo;

        public UnitofWork(DemoContext ctxt, ICountryRepository countryRepo, ICityRepository cityRepo)
        {
            this.ctxt = ctxt;
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;
        }

        public ICityRepository CityRepository => cityRepo;

        public ICountryRepository CountryRepository => countryRepo;

        public async Task Commit()
        {
            await ctxt.SaveChangesAsync();
        }
    }
}
