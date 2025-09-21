using Demo.Application.Interfaces;
using Demo.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IMediator mediator;

        public CityController(ICityService cityService, IMediator mediator)
        {
            this.cityService = cityService;
            this.mediator = mediator;
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
