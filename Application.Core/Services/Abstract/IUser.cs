using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Abstract
{
    public interface IUser
    {
        UserModel Register(string name, string lastName, string password, string email);
        UserModel AddNewUserToDB(UserModel user);
        bool Login(string email, string password);
        UserModel GetFirstUser();
    }
}
