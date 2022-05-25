using Application.Core.Models;
using Application.Core.Services.Abstract;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services.Concrete
{
    public class JobManager : IJobManager
    {
        private readonly DBaseContext _dbcontext;
        private readonly IUser _user;
        public JobManager(DBaseContext dbcontext, IUser user)
        {
            _dbcontext = dbcontext;
            _user = user;
        }

        public string RegisterNewUser(UserModelDTO userModel)
        {
            var createdUser = CreateUser(userModel);
            var jobID = BackgroundJob.Enqueue(() => _user.AddNewUserToDB(createdUser));
            return jobID;
        }

        public string RegisterNewUserWithDelay(UserModelDTO userModel)
        {
            var createdUser = CreateUser(userModel);
            var jobID = BackgroundJob.Schedule(() => _user.AddNewUserToDB(createdUser), TimeSpan.FromMinutes(2));
            return jobID;
        }

        private UserModel CreateUser(UserModelDTO userModel)
        {
            var createdUser = _user.Register(userModel.Name, userModel.LastName, userModel.password, userModel.email);
            return createdUser;
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
            CreateDelaysFixed();
        }

        public void CreateDelays()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<Books> booksWithDelays = _dbcontext.Books
                .Include(x => x.User)
                .Where(x => x.Bks_ReturnDate < DateTime.Now && !x.User.DelayedBooks
                .Select(z => z.Book).Contains(x))
                .ToList();

            if (booksWithDelays.Count == 0)
                return;

            foreach (var book in booksWithDelays)
            {
                DelayedBooks delayedBook = new DelayedBooks()
                {
                    Book = book,
                    User = book.User,
                };
                _dbcontext.DelayedBooks.Add(delayedBook);
            }
            _dbcontext.SaveChanges();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
        }

        public void CreateDelaysWithNplusOneProblem()
        {
            IList<UserModel> users = _dbcontext.Users
                .Where(x => x.Books != null)
                .ToList();

            if (users.Count == 0)
                return;

            foreach (var user in users)
            {
                foreach (var book in user.Books)
                {
                    DelayedBooks delayedBook = new DelayedBooks()
                    {
                        Book = book,
                        User = user,
                    };
                    _dbcontext.DelayedBooks.Add(delayedBook);
                }
            }
            _dbcontext.SaveChanges();
        }

        public void CreateDelaysFixed()
        {
            IList<UserModel> users = _dbcontext.Users
                .Include(x => x.Books)
                .Where(x => x.Books != null)
                .ToList();

            if (users.Count == 0)
                return;

            foreach (var user in users)
            {
                foreach (var book in user.Books)
                {
                    DelayedBooks delayedBook = new DelayedBooks()
                    {
                        Book = book,
                        User = user,
                    };
                    _dbcontext.DelayedBooks.Add(delayedBook);
                }
            }
            _dbcontext.SaveChanges();
        }
    }
}

