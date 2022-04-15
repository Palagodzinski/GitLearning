using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Abstract
{
    public interface IUserRepository
    {
        UserModel GetUserByMail(string email);
        UserModel AddNewUsertToDB(UserModel model);
        UserModel GetFirstUser();
    }
}
