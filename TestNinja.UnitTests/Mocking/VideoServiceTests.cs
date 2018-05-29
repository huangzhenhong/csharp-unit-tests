using NUnit.Framework;
using Moq;
using TestNinja.Mocking;
using System.Collections.Generic;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    [Category("VideoServiceTests")]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp() {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError() {
  
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideoAreProcessed_ReturnsEmptyString()
        {
            var videos = new List<Video>() {};

            _videoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(videos);

            var result = _videoService.GetUnprocessedVideosAsCsv();
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_OneOrMoreVideoAreNotProcessed_ReturnsIdsOfTheUnprocessedVideos() {
            var videos = new List<Video>() {
                new Video { Id = 0, IsProcessed = false, Title = "0" },
                new Video { Id = 1, IsProcessed = false, Title = "1" },
                new Video { Id = 2, IsProcessed = false, Title = "2" },
            };
            _videoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(videos);

            var result = _videoService.GetUnprocessedVideosAsCsv();
            Assert.That(result, Is.EqualTo("0,1,2"));
        }
    }
}
