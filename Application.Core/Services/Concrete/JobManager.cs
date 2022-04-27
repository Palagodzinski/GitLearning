using Application.Core.Models;
using Application.Core.Services.Abstract;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{
    public class JobManager : IJobManager
    {
        private readonly DBaseContext _dbcontext;
        public JobManager(DBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int DeleteRecurringJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in StorageConnectionExtensions.GetRecurringJobs(connection))
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
                int count = StorageConnectionExtensions.GetRecurringJobs(connection).Count();
                return count;
            }
        }

        public void VerifyDelays()
        {
            CreateDelays();
            //  RecurringJob.AddOrUpdate(() => CreateDelays(), Cron.Daily);
        }


        public void CreateDelays()
        {
            IList<Books> booksWithDelays = _dbcontext.Books.Include(x => x.User).Where(x => x.Bks_ReturnDate < DateTime.Now && x.User.Usr_ID == 2).ToList();

            if (booksWithDelays.Count == 0)
                return;

            foreach (var book in booksWithDelays)
            {
                var existingDelay = _dbcontext.DelayedBooks.Where(x => x.User == book.User && x.Book == book).FirstOrDefault();
                if (existingDelay != null)
                    continue;

                DelayedBooks delayedBook = new DelayedBooks()
                {
                    Book = book,
                    User = book.User,
                };
                _dbcontext.DelayedBooks.Add(delayedBook);
                _dbcontext.SaveChanges();

            }
        }
    }
}

