using Demo.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Demo.Application.CQRS.Cities;
using Demo.Application.Exceptions;

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
        [HttpGet]
        public async Task<IActionResult> GetCityById(int id)
        {
            try
            {
                var city = await mediator.Send(new GetCityByIdQuery() { Id = id });
                return Ok(city);
            }
            catch (CityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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
