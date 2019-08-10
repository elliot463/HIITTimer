using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PCLStorage;

namespace HIITTimer
{
    public class HIITRepository
    {
        readonly SQLiteAsyncConnection database;
        public HIITRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ProgramTable>().Wait();
            database.CreateTableAsync<IntervalTable>().Wait();
        }

        public Task<List<ProgramTable>> GetProgramsAsync()
        {
            return database.Table<ProgramTable>().OrderBy(a => a.DisplayOrder).ToListAsync();
        }
        public Task<int> SaveProgramAsync(ProgramTable item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        
        public Task<int> DeleteProgramAsync(ProgramTable item)
        {
            return database.DeleteAsync(item);
        }
        public Task<List<IntervalTable>> GetIntervals()
        {
            return database.Table<IntervalTable>().OrderBy(a => a.IntervalOrder).ToListAsync();
        }
        public Task<IntervalTable> GetIntervalByID(int iD)
        {
            var interval = from h in database.Table<IntervalTable>()
                           where h.ID.Equals(iD)
                           select h;
            return interval.FirstAsync();
        }
        public Task<int> SaveIntervalAsync(IntervalTable item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteIntervalAsync(IntervalTable item)
        {
            return database.DeleteAsync(item);
        }
        public Task<List<IntervalTable>> GetProgramIntervals(int programID)
        {
            var programIntervals = from h in database.Table<IntervalTable>()
                                   where h.ProgramID.Equals(programID)
                                   select h;
            return programIntervals.ToListAsync();
        }
        public Task<int> DeleteProgramIntervals(List<IntervalTable> intervals)
        {
            foreach (IntervalTable i in intervals)
            {
                database.DeleteAsync(i);
            }
            return Task.FromResult(intervals.Count);
        }
    }
}
