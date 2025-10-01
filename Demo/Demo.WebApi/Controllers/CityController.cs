using Demo.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Demo.Application.CQRS.Cities;
using Demo.Application.Exceptions;
using Demo.Domain;

namespace Demo.WebApi.Controllers
{
    public class CityController : APIv1Controller
    {
        private readonly IMediator mediator;

        public CityController(IMediator mediator)
        {
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

        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CreateCityDTO city)
        {
            try
            {
                var createdCity = await mediator.Send(new CreateCityCommand() { City = city});
                return Ok(createdCity);
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
            await mediator.Send(new DeleteCityCommand() { Id=id});
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
