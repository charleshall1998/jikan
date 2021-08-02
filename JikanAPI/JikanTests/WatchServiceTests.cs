using JikanAPI.Exceptions;
using JikanAPI.Models;
using JikanAPI.Repos.InMem;
using JikanAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JikanTests
{
    class WatchServiceTests
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
        public void AddWatchNullNameTest()
        {
            Watch toAdd = new Watch
            {
                Name = null,
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchEmptyNameTest()
        {
            Watch toAdd = new Watch
            {
                Name = "",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchTooLongNameTest()
        {
            Watch toAdd = new Watch
            {
                Name = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchWhiteSpaceNameTest()
        {
            Watch toAdd = new Watch
            {
                Name = "             ",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchNullTypeTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = null,
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<ArgumentNullException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchEmptyTypeTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchTooLongTypeTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchWhiteSpaceTypeTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "          ",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchInvalidPricePrecisionTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 1000.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidPriceException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void AddWatchInvalidPriceScaleTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.0000m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidPriceException>(() => _service.AddWatch(toAdd));
        }

        [Test]
        public void GetWatchByNameNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetWatchByName(null));
        }

        [Test]
        public void GetWatchByNameEmptyNameTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchByName(""));
        }

        [Test]
        public void GetWatchByNameTooLongTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchByName("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"));
        }

        [Test]
        public void GetWatchByNameWhiteSpaceNameTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchByName("        "));
        }

        [Test]
        public void GetWatchesByTypeNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetWatchesByType(null));
        }

        [Test]
        public void GetWatchesByTypeEmptyNameTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchesByType(""));
        }

        [Test]
        public void GetWatchesByTypeTooLongTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchesByType("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"));
        }

        [Test]
        public void GetWatchesByTypeWhiteSpaceNameTest()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetWatchesByType("        "));
        }

        [Test]
        public void EditWatchNullNameTest()
        {
            Watch toEdit = new Watch
            {
                Name = null,
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<ArgumentNullException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchEmptyNameTest()
        {
            Watch toEdit = new Watch
            {
                Name = "",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchTooLongNameTest()
        {
            Watch toEdit = new Watch
            {
                Name = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchWhiteSpaceNameTest()
        {
            Watch toEdit = new Watch
            {
                Name = "             ",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidNameException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchNullTypeTest()
        {
            Watch toEdit = new Watch
            {
                Name = "TEST NAME",
                Type = null,
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<ArgumentNullException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchEmptyTypeTest()
        {
            Watch toEdit = new Watch
            {
                Name = "TEST NAME",
                Type = "",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchTooLongTypeTest()
        {
            Watch toEdit = new Watch
            {
                Name = "TEST NAME",
                Type = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void EditWatchWhiteSpaceTypeTest()
        {
            Watch toEdit = new Watch
            {
                Name = "TEST NAME",
                Type = "          ",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Assert.Throws<InvalidTypeException>(() => _service.EditWatch(toEdit));
        }

        [Test]
        public void GetWatchesByOrderIdInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetWatchesByOrderId(-10000));
        }

        [Test]
        public void GetWatchQuantityByOrderIdInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetWatchQuantityByOrderId(-10000));
        }

        [Test]
        public void GetWatchQuantityPairInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetWatchQuantityPair(-10000));
        }

        [Test]
        public void GetWatchesByPriceTestInvalidPrecision()
        {
            Assert.Throws<InvalidPriceException>(() => _service.GetWatchesByPrice(100000.00m));
        }

        [Test]
        public void GetWatchesByPriceTestInvalidScale()
        {
            Assert.Throws<InvalidPriceException>(() => _service.GetWatchesByPrice(100.00000m));
        }
    }
}
