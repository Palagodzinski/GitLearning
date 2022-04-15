using Microsoft.AspNetCore.Mvc;
using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using Application.Core;
using System;
using Application.Core.Models;
using BenchmarkDotNet.Running;

namespace Application.Api.Controllers
{
    public class SimpleController : Controller
    {
        private readonly IImageFormatValidator _imageFormatValidator;
        private readonly IStringCreator _stringCreator;
        private readonly IUser _user;



        public SimpleController(IImageFormatValidator imageFormatValidator,
               IStringCreator stringCreator, IUser user)
        {
            _imageFormatValidator = imageFormatValidator;
            _stringCreator = stringCreator;
            _user = user;
        }

        [HttpGet("CheckIfFileIsInImageFormatRegex")]
        public async Task<IActionResult> ValidateFormat(string fileName)
        {
            var result = await _imageFormatValidator.ValidateFormat(fileName);
            return Ok(result);
        }

        [HttpGet("GetStringFromStringBuilder")]
        public async Task<IActionResult> CreateString()
        {
            var result = await _stringCreator.CreateString();
            return Ok(result);
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
    }
}
