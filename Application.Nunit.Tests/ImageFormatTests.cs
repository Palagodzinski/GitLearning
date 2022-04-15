using Application.Core.Services.Concrete;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.nUnit.Tests
{
    [TestFixture]
    public class ImageFormatTests
    {
        [Theory]
        [TestCase("To jest format pliku graficznego", "image.jpg")]
        [TestCase("To jest format pliku graficznego", "image.gif")]
        [TestCase("To nie jest format pliku graficznego", "image.xlsx")]
        [TestCase("To nie jest format pliku graficznego", "image.docx")]
        public void Validating_different_image_formats(string expected, string fileName)
        {
            //Arrange
            var sut = new ImageFormatValidator();

            //Act
            var act = sut.ValidateFormat(fileName);

            //Assert
            act.Result.Should().Be(expected);
        }
    }
}
