using SimpleTaskTracker_Data;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTaskTracker_Services
{

    public class TaskService : ITaskService<Task>
    {
        public TaskService()
        {
        }

        public void Add(Task item)
        {
            using (var db = new AppDBContext())
            {
                db.Tasks.Add(item);
                db.SaveChanges();
            }
        }

        public void Add(IEnumerable<Task> items)
        {
            using (var db = new AppDBContext())
            {
                db.Tasks.AddRange(items);
                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (var db = new AppDBContext())
            {
                var item = db.Tasks.First(f => f.Id == id);
                db.Tasks.Remove(item);
                db.SaveChanges();
            }
        }

        public void Delete(IEnumerable<long> id)
        {
            using (var db = new AppDBContext())
            {
                var Tasks = db.Tasks.Where(x => id.Contains(x.Id));
                db.Tasks.RemoveRange(Tasks);
                db.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            using (var db = new AppDBContext())
            {
                var Tasks = db.Tasks.Select(t => t);
                db.Tasks.RemoveRange(Tasks);
                db.SaveChanges();
            }
        }

        public Task Get(string name)
        {
            using (var db = new AppDBContext())
            {
                var Task = db.Tasks.First(t => t.TaskName == name);
                return Task;
            }
        }

        public IEnumerable<Task> List()
        {
            using (var db = new AppDBContext())
            {
                return db.Tasks.ToList();
            }
        }

        public void Update(Task item)
        {
            using (var db = new AppDBContext())
            {
                var itemInDB = db.Tasks.First(x => x.Id == item.Id);

                itemInDB.TaskName = item.TaskName;
                itemInDB.ClockIn = item.ClockIn;
                itemInDB.ClockOut = item.ClockOut;
                itemInDB.Total = item.Total;
                itemInDB.LastClosed = item.LastClosed;
                itemInDB.Selected = item.Selected;
                itemInDB.Hours = item.Hours;
                itemInDB.Minutes = item.Minutes;
                itemInDB.Seconds = item.Seconds;

                db.SaveChanges();
            }
        }
    }
}
