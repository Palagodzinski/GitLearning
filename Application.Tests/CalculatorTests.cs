using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentAssertions;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.xUnit.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Summing_two_integers()
        {
            //Arrange
            var sut = Substitute.For<ICalculatorEngine>();
            var calc = new Calculator(sut);
            sut.Add(2, 5).Returns(7);

            //Act
            var result = calc.Add(2, 5);

            //Assert
            result.Should().Be(7);
        }

        [Fact]
        public void Summing_two_decimals_using_Moq()
        {
            //Arrange
            var calcMock = new Mock<ICalculatorEngine>();
            calcMock.Setup(x => x.ConvertFromPLNtoUSD(2)).Returns(8.54m);
            var calc = new Calculator(calcMock.Object);

            //Act
            var result = calc.ConvertPLNtoUSD(2);

            //Assert
            Assert.True(result == 8.54m);
        }

        [Fact]
        public void Summing_two_decimals_using_Moq_and_AF()
        {
            //Arrange
            var calcMock = new Mock<ICalculatorEngine>();
            calcMock.Setup(x => x.ConvertFromPLNtoUSD(2)).Returns(8.54m);
            var calc = new Calculator(calcMock.Object);

            //Act
            var result = calc.ConvertPLNtoUSD(2);

            //Assert
            Assert.True(result == 8.54m);
        }
    }
}
