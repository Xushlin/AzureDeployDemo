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
            const int first = 1000;
            const int second = 2000;
            
            //Act
            const int sum = first + second;
            
            //Assert
            sum.Should().Be(3000);
        }
    }
}
