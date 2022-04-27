using Application.Core.Models;
using Application.Core.Services.Abstract;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    public class HangfireController : Controller
    {
        private readonly IUser _user;
        private readonly IJobManager _jobManager;
        public HangfireController(IUser user, IJobManager jobManager)
        {
            _jobManager = jobManager;
            _user = user;
        }

        [HttpPut("RegisterNewUserEnqueue")]
        public IActionResult Register([FromBody] UserModelDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = _user.Register(user.Name, user.LastName, user.password, user.email);
            var jobID = BackgroundJob.Enqueue(() => _user.AddNewUserToDB(createdUser));
            return Ok($"JobId :{jobID}  Completed.");
        }

        [HttpPut("RegisterNewUserDelayed")]
        public IActionResult RegisterWithDelay([FromBody] UserModelDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = _user.Register(user.Name, user.LastName, user.password, user.email);
            var jobID = BackgroundJob.Schedule(() => _user.AddNewUserToDB(createdUser), TimeSpan.FromMinutes(2));
            return Ok($"JobId : {jobID} Completed.");
        }

        [HttpDelete("DeleteReccuringJobs")]
        public IActionResult DeleteReccuringJobs()
        {
            var result = _jobManager.DeleteRecurringJobs();
            return Ok(result);
        }
    }
}
