using Demo.Application.Interfaces;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitofWork uow;
        public CityService(IUnitofWork uow)
        {
            this.uow = uow;
        }
        public async Task Delete(int id)
        {
            var city = await this.uow.CityRepository.GetById(id);
            if(city == null)
            {
                throw new KeyNotFoundException($"A city with id {id} does not exist!");
            }
            uow.CityRepository.Delete(city);
            await uow.Commit();
        }
    }
}
