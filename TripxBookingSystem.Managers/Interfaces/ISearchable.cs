using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.Models.Search;

namespace TripxBookingSystem.Managers.Interfaces
{
    public interface ISearchable
    {
        Task<SearchRes> SearchAsync(SearchReq request);
    }
}
