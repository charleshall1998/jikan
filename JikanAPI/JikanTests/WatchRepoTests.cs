using NUnit.Framework;
using JikanAPI.Repos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JikanAPI.Models;
using System.Collections.Generic;
using System;
using JikanAPI.Exceptions;

namespace JikanTests
{
    public class WatchRepoTests
    {
        WatchRepo _watchRepo;
        IServiceCollection _services = new ServiceCollection();

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<JikanDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            JikanDbContext context = new JikanDbContext(builder.Options);
            _watchRepo = new WatchRepo(context);

            context.Watches.RemoveRange(context.Watches);
            context.SaveChanges();
        }

        [Test]
        public void AddWatchTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Watch toAdd2 = new Watch
            {
                Name = "TEST NAME 2",
                Type = "TEST TYPE 2",
                Price = 100.00m,
                Description = "TEST DESCRIPTION 2",
                ImageUrl = "TEST URL 2",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);
            _watchRepo.AddWatch(toAdd2);

            List<Watch> toCheck = _watchRepo.GetAllWatches();

            Assert.AreEqual(2, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual("TEST TYPE", toCheck[0].Type);
            Assert.AreEqual(100.00m, toCheck[0].Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck[0].Description);
            Assert.AreEqual("TEST URL", toCheck[0].ImageUrl);


            Assert.AreEqual(toCheck[0].Id + 1, toCheck[1].Id);
            Assert.AreEqual("TEST NAME 2", toCheck[1].Name);
            Assert.AreEqual("TEST TYPE 2", toCheck[1].Type);
            Assert.AreEqual(100.00m, toCheck[1].Price);
            Assert.AreEqual("TEST DESCRIPTION 2", toCheck[1].Description);
            Assert.AreEqual("TEST URL 2", toCheck[1].ImageUrl);

        }

        [Test]
        public void AddNullWatchTest()
        {
            Assert.Throws<ArgumentNullException>(() => _watchRepo.AddWatch(null));
        }

        [Test]
        public void GetWatchByIdTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            Watch toCheck = _watchRepo.GetWatchById(toAdd.Id);

            Assert.AreEqual("TEST NAME", toCheck.Name);
            Assert.AreEqual("TEST TYPE", toCheck.Type);
            Assert.AreEqual(100.00m, toCheck.Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck.Description);
            Assert.AreEqual("TEST URL", toCheck.ImageUrl);
        }

        [Test]
        public void GetWatchByInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _watchRepo.GetWatchById(-10000));
        }

        [Test]
        public void GetWatchByInvalidIdDNETest()
        {
            Assert.Throws<WatchNotFoundException>(() => _watchRepo.GetWatchById(10));
        }

        [Test]
        public void GetWatchByNameTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            Watch toCheck = _watchRepo.GetWatchByName("TEST NAME");

            Assert.IsNotNull(toCheck.Id);
            Assert.AreEqual("TEST NAME", toCheck.Name);
            Assert.AreEqual("TEST TYPE", toCheck.Type);
            Assert.AreEqual(100.00m, toCheck.Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck.Description);
            Assert.AreEqual("TEST URL", toCheck.ImageUrl);
        }

        [Test]
        public void GetWatchByNullNameTest()
        {
            Assert.Throws<ArgumentNullException>(() => _watchRepo.GetWatchByName(null));
        }

        [Test]
        public void GetWatchByInvalidNameDNETest()
        {
            Assert.Throws<WatchNotFoundException>(() => _watchRepo.GetWatchByName("Test"));
        }

        [Test]
        public void GetAllWatchesTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            List<Watch> toCheck = _watchRepo.GetAllWatches();

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual("TEST TYPE", toCheck[0].Type);
            Assert.AreEqual(100.00m, toCheck[0].Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck[0].Description);
            Assert.AreEqual("TEST URL", toCheck[0].ImageUrl);
        }

        [Test]
        public void GetWatchesByTypeTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            List<Watch> toCheck = _watchRepo.GetWatchesByType("TEST TYPE");

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual("TEST TYPE", toCheck[0].Type);
            Assert.AreEqual(100.00m, toCheck[0].Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck[0].Description);
            Assert.AreEqual("TEST URL", toCheck[0].ImageUrl);
        }

        [Test]
        public void GetWatchesByNullTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => _watchRepo.GetWatchesByType(null));
        }

        [Test]
        public void GetWatchesByPriceTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            Watch toAdd2 = new Watch
            {
                Name = "TEST NAME 2",
                Type = "TEST TYPE 2",
                Price = 200.00m,
                Description = "TEST DESCRIPTION 2",
                ImageUrl = "TEST URL 2s",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);
            _watchRepo.AddWatch(toAdd2);

            List<Watch> toCheck = _watchRepo.GetWatchesByPrice(100.00m);

            Assert.AreEqual(1, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
            Assert.AreEqual("TEST TYPE", toCheck[0].Type);
            Assert.AreEqual(100.00m, toCheck[0].Price);
            Assert.AreEqual("TEST DESCRIPTION", toCheck[0].Description);
            Assert.AreEqual("TEST URL", toCheck[0].ImageUrl);
        }

        [Test]
        public void GetWatchesByPricelNegativeTest()
        {
            Assert.Throws<ArgumentException>(() => _watchRepo.GetWatchesByPrice(-100.00m));
        }

        [Test]
        public void EditWatchTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            Watch toEdit = toAdd;
            toEdit.Name = "NEW NAME";
            toEdit.Price = 200.00m;
            toEdit.Type = "NEW TYPE";
            toEdit.Description = "NEW DESCRIPTION";
            toEdit.ImageUrl = "NEW URL";

            _watchRepo.EditWatch(toEdit);

            List<Watch> toCheck = _watchRepo.GetAllWatches(); 

            Assert.AreEqual(1, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("NEW NAME", toCheck[0].Name);
            Assert.AreEqual("NEW TYPE", toCheck[0].Type);
            Assert.AreEqual(200.00m, toCheck[0].Price);
            Assert.AreEqual("NEW DESCRIPTION", toCheck[0].Description);
            Assert.AreEqual("NEW URL", toCheck[0].ImageUrl);
        }

        [Test]
        public void EditWatchNullWatchTest()
        {
            Assert.Throws<ArgumentNullException>(() => _watchRepo.EditWatch(null));
        }

        [Test]
        public void EditWatchInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _watchRepo.EditWatch(
                new Watch
                {
                    Id = -10000
                }
            ));
        }

        [Test]
        public void EditWatchInvalidIdDNETest()
        {
            Assert.Throws<WatchNotFoundException>(() => _watchRepo.EditWatch(
                new Watch
                {
                    Id = 10
                }
            ));
        }

        [Test]
        public void DeleteWatchTest()
        {
            Watch toAdd = new Watch
            {
                Name = "TEST NAME",
                Type = "TEST TYPE",
                Price = 100.00m,
                Description = "TEST DESCRIPTION",
                ImageUrl = "TEST URL",
                OrderDetails = new List<OrderDetail>()
            };

            _watchRepo.AddWatch(toAdd);

            _watchRepo.DeleteWatch(toAdd.Id);

            List<Watch> toCheck = _watchRepo.GetAllWatches();

            Assert.AreEqual(0, toCheck.Count);
        }

        [Test]
        public void DeleteWatchInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _watchRepo.DeleteWatch(-10000));
        }

        [Test]
        public void DeleteWatchInvalidIdDNETest()
        {
            Assert.Throws<WatchNotFoundException>(() => _watchRepo.DeleteWatch(10));
        }

        [Test]
        public void GetWatchQuantityByInvalidOrderIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _watchRepo.GetWatchQuantityByOrderId(-10000));
        }

        [Test]
        public void GetWatchQuantityByInvalidOrderIdDNETest()
        {
            Assert.Throws<InvalidIdException>(() => _watchRepo.GetWatchQuantityByOrderId(10));
        }
    }
}
