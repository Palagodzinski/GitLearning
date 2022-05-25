using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Abstract
{
    public interface IJobManager
    {
        int DeleteRecurringJobs();
        void VerifyDelays();
        string RegisterNewUser(UserModelDTO user);
        string RegisterNewUserWithDelay(UserModelDTO user);

    }
}
