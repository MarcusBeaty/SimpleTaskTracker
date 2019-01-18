using SimpleTaskTracker_Data;
using System;
using System.Linq;
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

        public void Filter(DateTime From, DateTime To)
        {
            Collection.Clear();
            
            foreach(var task in taskService.List())
            {
                // Getting Raw Clock-In database value
                var rawStartDate = (DateTime)task.ClockIn;

                // Converting to simplified DateTime object after converting to string.
                // String conversion simplifies it to "0/00/0000" format
                var startDate = Convert.ToDateTime(rawStartDate.ToString("d"));

                // Comparing Task date with user input 
                if( startDate >= From && startDate <= To)
                {
                    // Adding to Reports page if requirements are met
                    Collection.Add(task);
                }
            }
        }
    }
}
