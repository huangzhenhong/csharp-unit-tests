using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    [Category("HtmlFormatterTests")]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement() {

            var formatter = new HtmlFormatter();
            var result = formatter.FormatAsBold("abc");

            // specific 
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            // More general
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain("abc").IgnoreCase);
        }
    }
}
