using System.Linq;

namespace TestNinja.Mocking.Book
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedId = null);
    }
}