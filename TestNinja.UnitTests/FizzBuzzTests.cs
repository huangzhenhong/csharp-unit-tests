using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    [Category("FizzBuzzTests")]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(7, "7")]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz(int number, string result) {
            Assert.That(() => FizzBuzz.GetOutput(number) , Is.EqualTo(result));
        }
    }
}
