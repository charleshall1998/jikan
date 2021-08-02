using NUnit.Framework;
using JikanAPI.Repos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JikanAPI.Models.Auth;
using JikanAPI.Models;
using System.Collections.Generic;
using System;
using JikanAPI.Exceptions;

namespace JikanTests
{
    public class OrderRepoTests
    {
        OrderRepo _orderRepo;
        IServiceCollection _services = new ServiceCollection();
        User _user;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<JikanDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            JikanDbContext context = new JikanDbContext(builder.Options);
            _orderRepo = new OrderRepo(context);

            context.Orders.RemoveRange(context.Orders);
            context.SaveChanges();

            context.Users.RemoveRange(context.Users);

            //Add user here
            _user = new User
            {
                Username= "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME"
            };

            context.Users.Add(_user);
            context.SaveChanges();

            //_services.AddDbContext<JikanDbContext>(o => o.UseSqlServer(config.GetConnectionString("TestDb")));
            //_services.AddScoped<IOrderRepo, OrderRepo>();
        }

        [Test]
        public void AddOrderTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            Order toAdd2 = new Order
            {
                Name = "TEST NAME 2",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY 2",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            _orderRepo.AddOrder(toAdd);
            _orderRepo.AddOrder(toAdd2);

            List<Order> toCheck = _orderRepo.GetAllOrders();

            Assert.AreEqual(2, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual(0.0m, toCheck[0].Total);
            Assert.AreEqual("1234 TEST ROAD", toCheck[0].DeliveryAddress);
            Assert.AreEqual("test@test.com", toCheck[0].Email);
            Assert.AreEqual(12345, toCheck[0].PostalCode);
            Assert.AreEqual("TEST CITY", toCheck[0].City);
            Assert.AreEqual(_user, toCheck[0].Purchaser);

            Assert.AreEqual(toCheck[0].Id + 1, toCheck[1].Id);
            Assert.AreEqual("TEST NAME 2", toCheck[1].Name);
            Assert.AreEqual(0.0m, toCheck[1].Total);
            Assert.AreEqual("1234 TEST ROAD", toCheck[1].DeliveryAddress);
            Assert.AreEqual("test@test.com", toCheck[1].Email);
            Assert.AreEqual(12345, toCheck[1].PostalCode);
            Assert.AreEqual("TEST CITY 2", toCheck[1].City);
            Assert.AreEqual(_user, toCheck[1].Purchaser);
        }

        [Test]
        public void AddNullOrderTest()
        {
            Assert.Throws<ArgumentNullException>(() => _orderRepo.AddOrder(null));
        }

        [Test]
        public void GetOrderByIdTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            _orderRepo.AddOrder(toAdd);

            Order toCheck = _orderRepo.GetOrderById(toAdd.Id);

            Assert.IsNotNull(toCheck.Id);
            Assert.AreEqual("TEST NAME", toCheck.Name);
            Assert.AreEqual(0.0m, toCheck.Total);
            Assert.AreEqual("1234 TEST ROAD", toCheck.DeliveryAddress);
            Assert.AreEqual("test@test.com", toCheck.Email);
            Assert.AreEqual(12345, toCheck.PostalCode);
            Assert.AreEqual("TEST CITY", toCheck.City);
            Assert.AreEqual(_user, toCheck.Purchaser);
        }

        [Test]
        public void GetOrderByInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.GetOrderById(-10000));
        }

        [Test]
        public void GetOrderByInvalidIdDNETest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.GetOrderById(10));
        }

        [Test]
        public void GetAllOrdersTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            _orderRepo.AddOrder(toAdd);

            List<Order> toCheck = _orderRepo.GetAllOrders();

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual(1, toCheck.Count);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual(0.0m, toCheck[0].Total);
            Assert.AreEqual("1234 TEST ROAD", toCheck[0].DeliveryAddress);
            Assert.AreEqual("test@test.com", toCheck[0].Email);
            Assert.AreEqual(12345, toCheck[0].PostalCode);
            Assert.AreEqual("TEST CITY", toCheck[0].City);
            Assert.AreEqual(_user, toCheck[0].Purchaser);
        }

        [Test]
        public void DeleteOrderTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            _orderRepo.AddOrder(toAdd);

            _orderRepo.DeleteOrder(toAdd.Id);

            List<Order> toCheck = _orderRepo.GetAllOrders();

            Assert.AreEqual(0, toCheck.Count);
        }

        [Test]
        public void DeleteOrderInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.DeleteOrder(-10000));
        }

        [Test]
        public void DeleteOrderByInvalidIdDNETest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.DeleteOrder(10));
        }

        [Test]
        public void GetOrderByUserIdTest()
        {
            Order toAdd = new Order
            {
                Name = "TEST NAME",
                Total = 0.0m,
                DeliveryAddress = "1234 TEST ROAD",
                Email = "test@test.com",
                PostalCode = 12345,
                City = "TEST CITY",
                Purchaser = _user,
                OrderDetails = new List<OrderDetail>()
            };

            _orderRepo.AddOrder(toAdd);

            List<Order> toCheck = _orderRepo.GetOrdersByUserId(_user.Id);

            Assert.AreEqual(1, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual(0.0m, toCheck[0].Total);
            Assert.AreEqual("1234 TEST ROAD", toCheck[0].DeliveryAddress);
            Assert.AreEqual("test@test.com", toCheck[0].Email);
            Assert.AreEqual(12345, toCheck[0].PostalCode);
            Assert.AreEqual("TEST CITY", toCheck[0].City);
            Assert.AreEqual(_user, toCheck[0].Purchaser);
        }

        [Test]
        public void GetOrderByInvalidUserIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.GetOrdersByUserId(-10000));
        }

        [Test]
        public void GetOrderByInvalidUserIdDNETest()
        {
            Assert.Throws<InvalidIdException>(() => _orderRepo.GetOrdersByUserId(10));
        }
    }
}