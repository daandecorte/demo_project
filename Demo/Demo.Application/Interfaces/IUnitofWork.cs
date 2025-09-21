using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface IUnitofWork
    {
        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepository { get; }
        Task Commit();
    }
}
