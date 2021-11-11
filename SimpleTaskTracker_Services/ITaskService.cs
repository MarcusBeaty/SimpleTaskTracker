using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTaskTracker_Services
{
    public interface ITaskService<T> where T : class
    {
        Task<IEnumerable<T>> List();

        Task Update(T item);

        Task Add(T item);

        Task Add(IEnumerable<T> items);

        Task Delete(long id);

        Task Delete(IEnumerable<long> id);

        Task DeleteAll();
    }
}
