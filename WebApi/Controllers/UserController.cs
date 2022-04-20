using Application.Core.Models;
using Application.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPut("RegisterNewUser")]
        public IActionResult Register([FromBody] UserModelDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = _user.Register(user.Name, user.LastName, user.password, user.email);
            var newUser = _user.AddNewUserToDB(createdUser);
            return Ok(newUser);
        }

        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _user.Login(email, password);
            return Ok(result);
        }

        [HttpGet("GetFirstUser")]
        public IActionResult GetFirstDBUser()
        {
            var result = _user.GetFirstUser();
            return Ok(result);
        }

        [HttpGet("ChangeUserPassword")]
        public IActionResult ChangePassword(string email, string password, string newPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _user.ChangePassword(email, password, newPassword);
            return Ok(result);
        }

        [HttpPost("AddBookToUserAccount")]
        public IActionResult AddBookToUserAccount(Books book)
        {
            var result = _user.AddBookToUserAccount(book);
            return Ok(result);
        }
    }
}
