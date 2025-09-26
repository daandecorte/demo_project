using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface ICityService
    {
        public Task<IEnumerable<City>> GetAll();
        public Task<IEnumerable<City>> GetAllWithCountry();
        public Task<City?> GetById(int id);
        public Task<City> Add(City city);
        public Task Delete(int id);
    }
}
