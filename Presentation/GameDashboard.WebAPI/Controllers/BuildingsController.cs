using GameDashboard.Application.Features.Commands.CreateBuilding;
using GameDashboard.Application.Features.Commands.CreateBuildingTypes;
using GameDashboard.Application.Features.Commands.GetAllBuildingTypes;
using GameDashboard.Application.Features.Queries.GetAllByUserBuildings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private IMediator _mediator;


        public BuildingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateBuildingCommandRequest request)
        {

            request.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var response = await _mediator.Send(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("getallbyuser")]
        public async Task<IActionResult> GetAllByUser()
        {
            var request = new GetAllByUserBuildingsRequest();
            request.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var response = await _mediator.Send(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
