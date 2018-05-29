using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    [Category("MathTests")]
    public class MathTests
    {
        private Math _math;
        // SetUp: NUnit Test Runner will call this method before each Test
        // TearDown: Nunit Test Runner will call this method after each Test

        [SetUp]
        public void SetUp() {
            _math = new Math();
        }

        [Test]
        // [Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ReturnsTheSumOfArguments() {
            
            var result = _math.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
            // Assert.That(result, Is.Not.Null);
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult) {
            
            var result = _math.Max(a, b);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThenZero_ReturnOddNumbersUpToLimit() {

            var result = _math.GetOddNumbers(5);
            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
