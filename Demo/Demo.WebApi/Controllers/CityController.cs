using Demo.Application.Interfaces;
using Demo.Application.Services;
using MediatR;
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
        public async Task<IActionResult> GetAllCities()
        {
            return Ok(await mediator.Send(new GetAllCitiesQuery()));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            //exception handling for KeyNotFoundException in ExceptionHandlingMiddelware
            await cityService.Delete(id);
            return NoContent();
        }
    }
}
