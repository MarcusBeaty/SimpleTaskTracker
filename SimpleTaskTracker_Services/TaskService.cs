using SimpleTaskTracker_Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskModel = SimpleTaskTracker_Data.Task;

namespace SimpleTaskTracker_Services
{
    public class TaskService : ITaskService<TaskModel>
    {
        public async Task Add(TaskModel item)
        {
            using (var db = new AppDBContext())
            {
                db.Tasks.Add(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Add(IEnumerable<TaskModel> items)
        {
            using (var db = new AppDBContext())
            {
                db.Tasks.AddRange(items);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            using (var db = new AppDBContext())
            {
                var item = await db.Tasks.SingleAsync(f => f.Id == id);
                db.Tasks.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(IEnumerable<long> id)
        {
            using (var db = new AppDBContext())
            {
                var tasks = await db.Tasks.Where(x => id.Contains(x.Id)).ToListAsync();
                db.Tasks.RemoveRange(tasks);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAll()
        {
            using (var db = new AppDBContext())
            {
                var Tasks = await db.Tasks.ToListAsync();
                db.Tasks.RemoveRange(Tasks);
                await db.SaveChangesAsync();
            }
        }

        public async Task<TaskModel> Get(string name)
        {
            using (var db = new AppDBContext())
            {
                var Task = await db.Tasks.SingleAsync(t => t.TaskName == name);
                return Task;
            }
        }

        public async Task<IEnumerable<TaskModel>> List()
        {
            using (var db = new AppDBContext())
            {
                return db.Tasks.ToList();
            }
        }

        public async Task Update(TaskModel item)
        {
            using (var db = new AppDBContext())
            {
                var itemInDB = await db.Tasks.SingleAsync(x => x.Id == item.Id);

                itemInDB.TaskName = item.TaskName;
                itemInDB.ClockIn = item.ClockIn;
                itemInDB.ClockOut = item.ClockOut;
                itemInDB.Total = item.Total;
                itemInDB.LastClosed = item.LastClosed;
                itemInDB.Selected = item.Selected;
                itemInDB.Hours = item.Hours;
                itemInDB.Minutes = item.Minutes;
                itemInDB.Seconds = item.Seconds;

                await db.SaveChangesAsync();
            }
        }
    }
}
