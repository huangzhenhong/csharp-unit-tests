using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    [Category("OrderServiceTests")]
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreOrder() {
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            var order = new Order();
            orderService.PlaceOrder(order);

            // Test the interation between two objects
            storage.Verify(s => s.Store(order));
        }
    }
}
