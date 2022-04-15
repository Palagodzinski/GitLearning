using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using Moq;
using NUnit.Framework;
using NSubstitute;
using FluentAssertions;

namespace Application.Nunit.CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private ICalculatorEngine calcSub;

        [SetUp]
        public void Setup()
        {
            calcSub = Substitute.For<ICalculatorEngine>();
        }

        [Test]
        public void Summing_two_integers()
        {
            //Arrange
            var sut = new Calculator(calcSub);
            sut.Add(2, 5).Returns(7);

            //Act
            var result = sut.Add(2, 5);

            //Assert
            result.Should().Be(7);
        }
    }
}