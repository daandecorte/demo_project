using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class DeleteCityCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class DeleteCommandValidator: AbstractValidator<DeleteCityCommand>
    {
        private IUnitofWork uow;

        public DeleteCommandValidator(IUnitofWork uow)
        {
            this.uow = uow;
            RuleFor(c => c.Id).MustAsync(async (id, cancellation) =>
            {
                var city = await uow.CityRepository.GetById(id);
                return city != null;
            }).WithMessage(c=>$"A city with id {c.Id} does not exist!");

            RuleFor(c => c.Id).MustAsync(async (id, cancellation) =>
            {
                var cities = await uow.CityRepository.GetAll();
                return cities.Count() > 1;
            }).WithMessage("Cannot delete the last city.");
        }
    }
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
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
        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await uow.CityRepository.GetById(request.Id);
            uow.CityRepository.Delete(city);
            await uow.Commit();

            await emailService.SendEmailAsync(
                "daan.decorte@gmail.com",
                "City Deleted",
                $"The city '{city.Name}' has been deleted."
            );
            return Unit.Value;
        }
    }   
}
