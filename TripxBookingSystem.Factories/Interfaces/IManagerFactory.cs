using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripxBookingSystem.Managers.Interfaces;
using TripXBookingSystem.Models.Search;

namespace TripxBookingSystem.Factories.Interfaces
{
    public interface IManagerFactory
    {
        ISearchable GetManager(SearchReq request);
    }
}
