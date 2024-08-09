using GameDashboard.Application.Features.Commands.CreateBuildingTypes;
using GameDashboard.Application.Features.Commands.GetAllBuildingTypes;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]    
    public class BuildingTypesController : ControllerBase
    {
        private IMediator _mediator;

                
        public BuildingTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addseeddata")]
        public async Task<IActionResult> AddSeedData()
        {
            var request = new CreateBuildingTypesCommandRequest();
            var response = await _mediator.Send(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllBuildingTypesQueryRequest();
            request.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _mediator.Send(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
