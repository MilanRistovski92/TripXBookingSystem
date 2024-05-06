using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TripXBookingSystem.Models.CheckStatus;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckStatusController : ControllerBase
    {
        private readonly ICheckStatusService _checkStatusService;

        public CheckStatusController(ICheckStatusService checkStatusService)
        {
            _checkStatusService = checkStatusService;
        }

        [HttpGet("checkstatus")]
        public async Task<CheckStatusRes> CheckStatus([FromQuery] [Required] CheckStatusReq request)
        {
            return await _checkStatusService.CheckStatusAsync(request);
        }
    }
}
