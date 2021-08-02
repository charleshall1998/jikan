using System;
using System.Collections.Generic;
using System.Text;
using JikanAPI.Exceptions;
using JikanAPI.Models;
using JikanAPI.Repos.InMem;
using JikanAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace JikanTests
{
    public class OrderServiceTests
    {
        UserInMemRepo _userRepo;
        OrderInMemRepo _orderRepo;
        WatchInMemRepo _watchRepo;

        IServiceCollection _services = new ServiceCollection();
        JikanService _service;

        [SetUp]
        public void Setup()
        {
            _userRepo = new UserInMemRepo();
            _orderRepo = new OrderInMemRepo();
            _watchRepo = new WatchInMemRepo();

            _service = new JikanService(_orderRepo, _watchRepo, _userRepo);
        }

        [Test]
        public void AddOrderNullNameTest()
        {
            Order toAdd = new Order
            {
                Name = null,
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderEmptyNameTest()
        {
            Order toAdd = new Order
            {
                Name = "",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderWhitespaceNameTest()
        {
            Order toAdd = new Order
            {
                Name = "          ",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderNullCityTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = null
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderEmptyCityTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = ""
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderWhitespaceCityTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "          "
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderNullDeliveryAddressTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = null,
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderEmptyDeliveryAddressTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderWhitespaceDeliveryAddressTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "           ",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderNullEmailTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = null,
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderEmptyEmailTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderWhitespaceEmailTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "          ",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderInvalidPricePrecisionTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100000.00m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidPriceException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void AddOrderInvalidPriceScaleTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 100.00000m,
                DeliveryAddress = "TEST ADDRESS",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY"
            };

            Assert.Throws<InvalidPriceException>(() => _service.AddOrder(toAdd));
        }

        [Test]
        public void GetOrderByIdInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetOrderById(-10000));
        }

        [Test]
        public void GetOrdersByUserIdInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetOrdersByUserId(-10000));
        }

        [Test]
        public void DeleteOrderInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.DeleteOrder(-10000));
        }
    }
}
