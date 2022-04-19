﻿using Application.Core.Models;
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

        public UserModel AddNewUsertToDB(UserModel user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();


            var newUser = _dbContext.Users.Where(x => x.Usr_ID == user.Usr_ID).FirstOrDefault();
            var query = _dbContext.Users.Where(x => x.Usr_ID > 1).ToHashSet();
            var list = _dbContext.Users.ToHashSet();
            var sss = _dbContext.Users.Where(x => x.Usr_ID != 0).Include(x => x.Usr_LastName).ToHashSet();
            return newUser;
        }

        public UserModel GetFirstUser()
        {
            var user = _dbContext.Users.FirstOrDefault();
            return user;
        }
        public UserModel GetUserByMail(string email)
        {
            var user = _dbContext.Users.Where(x => x.Usr_Email == email).FirstOrDefault();
            return user;
        }
    }
}
