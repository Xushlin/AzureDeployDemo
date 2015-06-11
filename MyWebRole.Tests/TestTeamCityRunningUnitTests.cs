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
        
        [Test]
        public void Test1()
        {
            //Arrange
             decimal first = 1000;
             int second = 1000;
            
            //Act
            var result = first.Equals(second);
            var result1 = first == second;
            //Assert
            result.Should().BeTrue();
            result1.Should().BeTrue();
        }
    }
}
