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

        public SimpleController(IImageFormatValidator imageFormatValidator,
               IStringCreator stringCreator)
        {
            _imageFormatValidator = imageFormatValidator;
            _stringCreator = stringCreator;
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
    }
}
