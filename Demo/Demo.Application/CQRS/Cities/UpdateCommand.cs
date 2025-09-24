using Demo.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class UpdateCommand: IRequest<CityDTO>
    {
        public CityDTO City { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        private IUnitofWork uow;

        public UpdateCommandValidator(IUnitofWork uow)
        {
            this.uow = uow;

            RuleFor(s => s.City.Name)
                .NotEmpty().WithMessage("De naam mag niet leeg zijn.");

            RuleFor(s => s.City.Population)
                .InclusiveBetween(0, 10000000000).WithMessage("Het aantal inwoners mag niet groter zijn dan 10.000.000.000.");

            RuleFor(s => s.City.CountryId)
                .GreaterThan(0).WithMessage("Er moet een land gekozen worden.");
        }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CityDTO>
    {
        private IUnitofWork uow;
        private IMapper mapper;
        public UpdateCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<CityDTO> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var city = await uow.CityRepository.GetById(request.City.Id);
            if (city == null)
                throw new Exception("City not found");
            // Map the updated fields from the DTO to the entity
            mapper.Map(request.City, city);
            var updatedCity = uow.CityRepository.Update(city);
            await uow.Commit();
            return mapper.Map<CityDTO>(city);
        }
    }
}
