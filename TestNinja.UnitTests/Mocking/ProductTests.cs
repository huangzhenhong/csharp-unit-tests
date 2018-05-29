using System;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    [Category("ProductTests")]
    public class ProductTests
    {
        private Customer _customer;
        private Product _product;

        [SetUp]
        public void SetUp() {
            _customer = new Customer();
            _product = new Product()
            {
                ListPrice = 100
            };
        }

        [Test]
        public void GetPrice_CustomerIsGold_Gets30PercentDiscount()
        {
            _customer.IsGold = true;

            Assert.That(_product.GetPrice(_customer), Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_CustomerIsNotGold_GetsListPrice()
        {
            _customer.IsGold = false;

            Assert.That(_product.GetPrice(_customer), Is.EqualTo(100));
        }
    }
}
