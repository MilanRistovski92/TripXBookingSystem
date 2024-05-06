using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripXBookingSystem.DataStore.Implementations;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Services.Interfaces;

namespace TripXBookingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private IDataStore<string> _tokenDataStore;


        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
            _tokenDataStore = DataStores.TokenResults;
        }

        [AllowAnonymous]
        [HttpGet("token")]
        public async Task<string> GenerateToken()
        {
            var token = await Task.Run(() => _tokenService.GenerateToken());
            _tokenDataStore.StoreSingleTokenResult("token", token);
            return token;
        }
    }
}
