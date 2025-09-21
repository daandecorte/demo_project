using MediatR;
using Demo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Demo.Application.CQRS.Cities;

namespace Demo.WebApi.Controllers
{
    public class CityController : APIv1Controller
    {
        private readonly ICityService cityService;
        private readonly IMediator mediator;

        public CityController(ICityService cityService, IMediator mediator)
        {
            this.cityService = cityService;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] int pageNr = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await mediator.Send(new GetAllCitiesQuery() { PageNr = pageNr, PageSize = pageSize}));
        }
    }
}
