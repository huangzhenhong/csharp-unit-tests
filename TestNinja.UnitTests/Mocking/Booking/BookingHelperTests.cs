using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking.Book;

namespace TestNinja.UnitTests.Mocking.Book
{
    [TestFixture]
    [Category("BookingHelperTests")]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _repository;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp() {
            _repository = new Mock<IBookingRepository>();
            _existingBooking =
                new Booking
                {
                    Id = 1,
                    Status = "Active",
                    ArrivalDate = ArrvialOn(2018, 6, 10),
                    DepartureDate = DepartureOn(2018, 6, 20),
                    Reference = "ExistingBooking",
                };
            _repository.Setup(r => r.GetActiveBookings(2)).Returns(new List<Booking> { _existingBooking }.AsQueryable());
        }

        [Test]
        public void CancelledBooking_ReturnsEmptyString() {

            var result = BookingHelper.OverlappingBookingsExist(new Booking { Status = "Cancelled" }, _repository.Object);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsAndEndsBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var booking = new Booking {
                Id = 2,
                Status = "Active",
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate, days: 1),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeEndsInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 2,
                Status = "Active",
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.ArrivalDate, days: 1),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeEndsAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 2,
                Status = "Active",
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 1),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndEndsInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 2,
                Status = "Active",
                ArrivalDate = After(_existingBooking.ArrivalDate, 1),
                DepartureDate = Before(_existingBooking.DepartureDate, 1),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleAndEndsAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 2,
                Status = "Active",
                ArrivalDate = After(_existingBooking.ArrivalDate, 1),
                DepartureDate = After(_existingBooking.DepartureDate, 1),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndEndsAfterAnExistingBooking_ReturnsEmptyString()
        {
            var booking = new Booking
            {
                Id = 2,
                Status = "Active",
                ArrivalDate = After(_existingBooking.DepartureDate, 1),
                DepartureDate = After(_existingBooking.DepartureDate, 2),
                Reference = "NewBooking"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1) {
            return dateTime.AddDays(days);
        }

        private DateTime ArrvialOn(int year, int month, int day) {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartureOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
