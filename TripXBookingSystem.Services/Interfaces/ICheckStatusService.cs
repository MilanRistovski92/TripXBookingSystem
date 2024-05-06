using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.CheckStatus;

namespace TripXBookingSystem.Services.Interfaces
{
    public interface ICheckStatusService
    {
        Task<CheckStatusRes> CheckStatusAsync(CheckStatusReq request);
    }
}
