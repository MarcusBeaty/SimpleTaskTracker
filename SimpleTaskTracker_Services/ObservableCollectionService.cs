using SimpleTaskTracker_Data;
using System;
using System.Collections.ObjectModel;


namespace SimpleTaskTracker_Services
{
    public class ObservableCollectionService : IObservableCollectionService<Task>
    {
        public static ObservableCollection<Task> Collection = new ObservableCollection<Task>();
        private TaskService taskService;

        public ObservableCollectionService()
        {
            taskService = new TaskService();
        }

        public void Add(Task item)
        {
            throw new NotImplementedException();
            
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            try
            {
                // This method is called whenever there is a modification to the database || Populating DataGrid from Recreated Tabs
                Collection.Clear();
                // Iterating through database and adding entries to Obseravable Collection
                foreach (var itm in taskService.List())
                {
                    Collection.Add(itm);
                }
            }
            catch
            {
            }
           
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
