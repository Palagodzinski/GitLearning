using Application.Core.Models;
using Application.Core.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{
    public class UserRepository : IUserRepository
    {
        private DBaseContext _dbContext;
        public UserRepository(IServiceProvider provider, DBaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public UserModel? AddNewUsertToDB(UserModel user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            var newUser = _dbContext.Users.Where(x => x.Usr_ID == user.Usr_ID).FirstOrDefault();

            return newUser;
        }

        public UserModel GetFirstUser()
        {
            var user = _dbContext.Users.FirstOrDefault();
            return user;
        }
        public UserModel? GetUserByMail(string email)
        {
            var user = _dbContext.Users.Where(x => x.Usr_Email == email).FirstOrDefault();
            if (user != null)
                return user;
            return null;
        }

        public string? ChangePassword(string email, string password, string newPassword)
        {
            var user = _dbContext.Users.Where(x => x.Usr_Email == email && x.Usr_Password == password).FirstOrDefault();
            if (user != null)
            {
                user.Usr_Password = newPassword;
                _dbContext.SaveChanges();
                return user.Usr_Password;
            }
            return null;
        }

        public int? AddBookToUserAccount(Books book)
        {
            if (book != null)
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
                return book.Bks_ID;
            }
            return null;
        }
    }
}
