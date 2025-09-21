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
        public async Task Delete(int id)
        {
            var city = await this.uow.CityRepository.GetById(id);
            if(city == null)
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
