using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    [Category("EmployeeControllerTests")]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _employeeController;

        [SetUp]
        public void SetUp() {
            _storage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb() {

            Assert.That(() => _employeeController.DeleteEmployee(It.IsAny<int>()), Is.InstanceOf<RedirectResult>());
            _storage.Verify(s => s.DeleteEmployee(It.IsAny<int>()));
        }
    }
}
