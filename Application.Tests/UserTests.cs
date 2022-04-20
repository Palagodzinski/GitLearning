using Application.Api;
using Application.Api.Controllers;
using Application.Core;
using Application.Core.Models;
using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.xUnit.Tests
{
    public class UserTests
    {
        private readonly Mock<IUser> userMock;
        private readonly Mock<IStringCreator> creatorMock;
        private readonly Mock<IImageFormatValidator> imageMock;

        public UserTests()
        {
            imageMock = new Mock<IImageFormatValidator>();
            creatorMock = new Mock<IStringCreator>();
            userMock = new Mock<IUser>();
        }

        [Fact]
        public void Adding_new_user_to_DB_with_valid_values()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var test = Substitute.For<IServiceProvider>();
            var data = fixture.Create<UserModel>();
            var sut = fixture.Create<User>();

            //Act
            var user = sut.Register(data.Usr_Name, data.Usr_LastName, data.Usr_Password, data.Usr_Email);

            //Assert
            user.Should().NotBeNull();
        }

        [Fact]
        public void Logging_in_with_invalid_credentials()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var calMock = new Mock<IUserRepository>();
            var sut = new User(calMock.Object);
            var user = fixture.Create<UserModel>();

            //Act
            var result = sut.Login(user.Usr_Email, user.Usr_Password);

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void Validating_wrong_values()
        {
            //Arrange
            var fixture = new Fixture();

            var validator = new UserRegisterValidator();
            var sut = new UserController(userMock.Object);
            var user = fixture.Create<UserModelDTO>();

            //Act
            var result = validator.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validating_right_values()
        {
            //Arrange
            var fixture = new Fixture();

            var validator = new UserRegisterValidator();
            var sut = new UserController(userMock.Object);
            var user = new UserModelDTO
            {
                email = "pawel@o2.pl",
                Name = "Pawel",
                LastName = "Lagodzinski",
                password = "pawell1996wy"
            };

            //Act
            var result = validator.Validate(user);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
