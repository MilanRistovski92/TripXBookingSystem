using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.CheckStatus;

namespace TripxBookingSystem.Managers.Interfaces
{
    //REMOVE
    public interface IStatusCheckable
    {
        Task<CheckStatusRes> CheckStatusAsync(CheckStatusReq request);
    }
}
