using Demo.Application.Interfaces;
using Demo.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Demo.Application.CQRS.Cities;
using Demo.Application.Exceptions;
using Demo.Domain;

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
            var city = await mediator.Send(new GetCityByIdQuery() { Id = id });

            if (city == null)
                return NotFound();

            return Ok(city);
        }


        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            //exception handling for KeyNotFoundException in ExceptionHandlingMiddelware
            await cityService.Delete(id);
            return NoContent();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityDTO city)
        {
            if (id != city.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var updatedCity = await mediator.Send(new UpdateCommand { City = city });

            return Ok(updatedCity);
        }
    }
}
