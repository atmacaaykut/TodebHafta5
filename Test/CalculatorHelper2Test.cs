using Bussines.Configuration.Helper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xunit;

namespace Test
{
    public class CalculatorHelper2Test
    {
        [Theory]
        [InlineData(100,8)]
        [InlineData(100,18)]
        [InlineData(50,10)]
        public void CalculatorVat_Success(decimal price,float rate)
        {
            
            //act
            var calculator = new CalculatorHelper2();
           var response=calculator.CalculateVAT(price, rate);

            var actual=(price * (decimal)rate) / 100;

            //assert
            //Assert.True(response>0);
            //Assert.Equal(response, actual);

            response.Should().BePositive();
            response.Should().Be(actual);
        }
    }
}
