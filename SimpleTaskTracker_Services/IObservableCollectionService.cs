using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SimpleTaskTracker_Services
{
    public interface IObservableCollectionService<T> where T : class
    {
        void Add(T item);

        void Delete();

        void Update();

        void Refresh();
    }
}
