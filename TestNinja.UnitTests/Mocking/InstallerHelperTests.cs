using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    [Category("InstallerHelperTests")]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _helper;

        [SetUp]
        public void SetUp() {
            _fileDownloader = new Mock<IFileDownloader>();
            _helper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadSucceed_ReturnsTrue() {
            var result = _helper.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFailed_ReturnsFalse()
        {

            _fileDownloader.Setup(
                fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<System.Net.WebException>();

            var result = _helper.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.False);
        }
    }
}
