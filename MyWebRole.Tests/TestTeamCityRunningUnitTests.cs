using FluentAssertions;
using NUnit.Framework;

namespace MyWebRole.Tests
{
    [TestFixture]
    public class TestTeamCityRunningUnitTests
    {
        [Test]
        public void Test()
        {
            //Arrange
            var first = 1000;
            var second = 2000;
            
            //Act
            var sum = first + second;
            
            //Assert
            sum.Should().Be(3000);
        }
    }
}
