using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking.Book
{
    public class BookingRepository : IBookingRepository
    {
        private IUnitOfWork _unitOfWork;
        public BookingRepository(IUnitOfWork unitOfWork = null) {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBookings(int? excludedId = null) {

            var bookings = _unitOfWork.Query<Booking>()
                           .Where(b => b.Status != "Cancelled");

            if (excludedId.HasValue) {
                bookings = bookings.Where(b => b.Id != excludedId.Value);
            }

            return bookings;
        }
    }
}
