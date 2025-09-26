using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class DeleteCityCommand: IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        public DeleteCityCommandHandler(IUnitofWork uow, IMapper mapper, IEmailService emailService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.emailService = emailService;
        }
        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var cities = await this.uow.CityRepository.GetAll();
            if (cities.Count() <= 1)
            {
                throw new ValidationException("Cannot delete the last city.");
            }
            var city = cities.FirstOrDefault(c => c.Id == request.Id);
            if (city == null)
            {
                throw new KeyNotFoundException($"A city with id {request.Id} does not exist!");
            }
            uow.CityRepository.Delete(city);
            await uow.Commit();

            var subject = "City Deleted";
            var body = $"The city '{city.Name}' has been deleted.";
            await emailService.SendEmailAsync("daan.decorte@gmail.com", subject, body);
        }
    }   
}
