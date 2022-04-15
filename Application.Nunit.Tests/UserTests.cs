using Application.Api;
using Application.Api.Controllers;
using Application.Core.Models;
using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.nUnit.Tests
{
    [TestFixture]
    public class UserTests
    {
        private Mock<IUser>? _userMock;
        private Mock<IStringCreator>? _creatorMock;
        private Mock<IImageFormatValidator>? _imageMock;

        [SetUp]
        public void SetUp()
        {
            _imageMock = new Mock<IImageFormatValidator>();
            _creatorMock = new Mock<IStringCreator>();
            _userMock = new Mock<IUser>();
        }

        [Test]
        public void Adding_new_user_to_DB_with_valid_values()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var user = fixture.Create<UserModel>();
            var sut = fixture.Create<User>();

            //Act
            var result = sut.AddNewUserToDB(user);

            //Assert
            result.Should().NotBeNull();
        }

        [Test]
        public void Validating_wrong_values()
        {
            //Arrange
            var fixture = new Fixture();
            var validator = new UserRegisterValidator();
            var sut = new SimpleController(_imageMock.Object, _creatorMock.Object, _userMock.Object);
            var user = fixture.Create<UserModelDTO>();

            //Act
            var result = validator.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Test]
        public void Checking_the_number_of_calls_to_AddNewUsertToDB_method()
        {
            //Arrange
            var fixture = new Fixture();
            var sub = Substitute.For<IUserRepository>();
            var sut = new User(sub);
            var user = fixture.Create<UserModel>();

            //Act
            var act = sut.AddNewUserToDB(user);

            //Assert
            sub.Received(1).AddNewUsertToDB(user);
        }
    }
}
