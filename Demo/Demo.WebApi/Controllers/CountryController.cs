using Demo.Application.CQRS.Cities;
using Demo.Application.CQRS.Countries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.Controllers
{
    public class CountryController : APIv1Controller
    {
        private readonly IMediator mediator;

        public CountryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery] int pageNr = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await mediator.Send(new GetAllCountriesQuery(){  PageNr = pageNr, PageSize = pageSize }));
        }

        [Route("{id}/cities")]
        [HttpGet]
        public async Task<IActionResult> GetCitiesByCountry(int id)
        {
            return Ok(await mediator.Send(new GetAllCitiesByCountryQuery() { Id = id} ));
        }

    }
}
