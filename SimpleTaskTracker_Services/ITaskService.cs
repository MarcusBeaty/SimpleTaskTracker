using System;
using System.Collections.Generic;

namespace SimpleTaskTracker_Services
{
    public interface ITaskService<T> where T : class
    {
        IEnumerable<T> List();

        void Update(T item);

        void Add(T item);

        void Add(IEnumerable<T> items);

        void Delete(long id);

        void Delete(IEnumerable<long> id);

        void DeleteAll();
    }
}
