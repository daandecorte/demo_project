using Demo.Application.Interfaces;
using Demo.Domain;

namespace Demo.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitofWork uow;

        public CityService(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<City>> GetAll(int pageNr, int pageSize)
        {
            return await uow.CityRepository.GetAll(pageNr, pageSize);
        }

        public async Task<City> GetById(int id)
        {
            return await uow.CityRepository.GetById(id);
        }

        public Task<City> Add(City city)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task<City> Update(City city)
        {
            throw new NotImplementedException();
        }
    }
}
