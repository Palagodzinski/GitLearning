using Application.Core.Models;
using Application.Core.Services.Abstract;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    public class HangfireController : Controller
    {
        private readonly IUser _user;
        public HangfireController(IUser user)
        {
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

        [HttpPut("RegisterNewUserCron")]
        public IActionResult RegisterCron([FromBody] UserModelDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = _user.Register(user.Name, user.LastName, user.password, user.email);
            RecurringJob.AddOrUpdate(() => _user.AddNewUserToDB(createdUser), Cron.Minutely);
            return Ok($"ReccuringJob Scheduled (Minutely)");
        }
    }
}
