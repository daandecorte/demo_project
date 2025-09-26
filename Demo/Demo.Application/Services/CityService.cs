using Demo.Application.Exceptions;
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
        private readonly IEmailService emailService;
        public CityService(IUnitofWork uow, IEmailService emailService)
        {
            this.uow = uow;
            this.emailService = emailService;
        }

        public async Task<IEnumerable<City>> GetAllWithCountry()
        {
            return await uow.CityRepository.GetAllWithCountry();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await uow.CityRepository.GetAll();
        }

        public async Task<City> GetById(int id)
            {
            return await uow.CityRepository.GetById(id);
            }

        public Task<City> Add(City city)
            {
            throw new NotImplementedException();
            }

        public async Task Delete(int id)
        {
            var cities = await this.uow.CityRepository.GetAll();
            if (cities.Count() <= 1)
            {
                throw new ValidationException("Cannot delete the last city.");
            }
            var city = cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                throw new KeyNotFoundException($"A city with id {id} does not exist!");
            }
            uow.CityRepository.Delete(city);
            await uow.Commit();

            var subject = "City Deleted";
            var body = $"The city '{city.Name}' has been deleted.";
            await emailService.SendEmailAsync("daan.decorte@gmail.com", subject, body);
        }
    }
}
