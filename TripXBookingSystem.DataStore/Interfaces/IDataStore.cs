using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripXBookingSystem.DataStore.Interfaces
{
    public interface IDataStore<T>
    {
        void StoreSingleResult(string key, T result);
        T GetSingleResult(string key);
        void AddResult(string key, T result);
        void ClearResults();
        void Update(string key, T updatedResult, Predicate<T> match);
        public void StoreSingleTokenResult(string key, T result);
    }
}
