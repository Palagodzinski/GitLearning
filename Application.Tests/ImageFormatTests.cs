using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class ImageFormatTests
    {
        [Theory]
        [InlineData("To jest format pliku graficznego", "image.jpg")]
        [InlineData("To jest format pliku graficznego", "image.gif")]
        [InlineData("To nie jest format pliku graficznego", "image.xlsx")]
        [InlineData("To nie jest format pliku graficznego", "image.docx")]
        public void Validating_different_image_formats(string expected, string fileName)
        {
            //Arrange
            var sut = new ImageFormatValidator();

            //Act
            var act = sut.ValidateFormat(fileName);

            //Assert
            act.Result.Should().Be(expected);
        }

        [Fact]
        public void Throwing_argument_null_exception_when_filename_is_null()
        {
            //Arrange
            var sut = new ImageFormatValidator();

            //Act
            Func<Task<string>> act = () => sut.ValidateFormat(null);

            //Assert
            act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}