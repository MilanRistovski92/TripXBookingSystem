using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripXBookingSystem.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken();
    }
}
