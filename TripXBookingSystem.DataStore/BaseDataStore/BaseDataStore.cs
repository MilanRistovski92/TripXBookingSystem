using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripXBookingSystem.DataStore.Interfaces;

namespace TripXBookingSystem.DataStore.BaseDataStore
{
    public class BaseDataStore<T> : IDataStore<T>
    {
        private readonly Dictionary<string, List<T>> _listStore = new Dictionary<string, List<T>>();
        private readonly Dictionary<string, T> _singleItemStore = new Dictionary<string, T>();

        public void StoreSingleResult(string key, T result)
        {
            lock (_singleItemStore)
            {
                _singleItemStore[key] = result;
            }
        }

        public void StoreSingleTokenResult(string key, T result)
        {
            lock (_singleItemStore)
            {
                _singleItemStore[key] = result;
            }
        }

        public T GetSingleResult(string key)
        {
            lock (_singleItemStore)
            {
                if (_singleItemStore.TryGetValue(key, out var result))
                {
                    return result;
                }
                return default;
            }
        }

        public void AddResult(string key, T result)
        {
            lock (_listStore)
            {
                if (_listStore.TryGetValue(key, out var results))
                {
                    results.Add(result);
                }
                else
                {
                    _listStore[key] = new List<T> { result };
                }
            }
        }

        public void Update(string key, T updatedResult, Predicate<T> match)
        {
            lock (_singleItemStore)
            {
                if (_singleItemStore.TryGetValue(key, out var currentItem))
                {
                    if (match(currentItem))
                    {
                        _singleItemStore[key] = updatedResult;
                    }
                }
                else
                {
                    throw new KeyNotFoundException($"No entry found with key {key} to update.");
                }
            }
        }

        public void ClearResults()
        {
            lock (_listStore)
            {
                _listStore.Clear();
            }
        }
    }
}
