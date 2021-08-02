using NUnit.Framework;
using JikanAPI.Repos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JikanAPI.Models.Auth;
using System.Collections.Generic;
using System;
using JikanAPI.Exceptions;

namespace JikanTests
{
    public class UserRepoTests
    {
        UserRepo _userRepo;
        IServiceCollection _services = new ServiceCollection();

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<JikanDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            JikanDbContext context = new JikanDbContext(builder.Options);
            _userRepo = new UserRepo(context);

            context.Orders.RemoveRange(context.Orders);

            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }

        [Test]
        public void AddUserTest()
        {
            User toAdd = new User
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME"
            };

            User toAdd2 = new User
            {
                Username = "TEST USERNAME 2",
                Email = "test2@test.com",
                Name = "TEST NAME 2"
            };

            _userRepo.AddUser(toAdd);
            _userRepo.AddUser(toAdd2);


            List<User> toCheck = _userRepo.GetAllUsers();

            Assert.AreEqual(2, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST USERNAME", toCheck[0].Username);
            Assert.AreEqual("test@test.com", toCheck[0].Email);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);

            Assert.AreEqual(toAdd.Id + 1, toCheck[1].Id);
            Assert.AreEqual("TEST USERNAME 2", toCheck[1].Username);
            Assert.AreEqual("test2@test.com", toCheck[1].Email);
            Assert.AreEqual("TEST NAME 2", toCheck[1].Name);
        }

        [Test]
        public void AddNullUserTest()
        {
            Assert.Throws<ArgumentNullException>(() => _userRepo.AddUser(null));
        }

        [Test]
        public void GetAllUsersTest()
        {
            User toAdd = new User
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME"
            };

            _userRepo.AddUser(toAdd);

            List<User> toCheck = _userRepo.GetAllUsers();

            Assert.AreEqual(1, toCheck.Count);

            Assert.IsNotNull(toCheck[0].Id);
            Assert.AreEqual("TEST USERNAME", toCheck[0].Username);
            Assert.AreEqual("test@test.com", toCheck[0].Email);
            Assert.AreEqual("TEST NAME", toCheck[0].Name);
        }

        [Test]
        public void GetUserByIdTest()
        {
            User toAdd = new User
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME"
            };

            _userRepo.AddUser(toAdd);

            User toCheck = _userRepo.GetUserById(toAdd.Id);

            Assert.IsNotNull(toCheck.Id);
            Assert.AreEqual("TEST USERNAME", toCheck.Username);
            Assert.AreEqual("test@test.com", toCheck.Email);
            Assert.AreEqual("TEST NAME", toCheck.Name);
        }

        [Test]
        public void GetUserByInvalidIdNegativeTest()
        {
            Assert.Throws<InvalidIdException>(() => _userRepo.GetUserById(-10000));
        }

        [Test]
        public void GetUserByInvalidIdDNETest()
        {
            Assert.Throws<InvalidIdException>(() => _userRepo.GetUserById(10));
        }

        [Test]
        public void GetUserByUsernameTest()
        {
            User toAdd = new User
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME"
            };

            _userRepo.AddUser(toAdd);

            User toCheck = _userRepo.GetUserByUsername("TEST USERNAME");

            Assert.IsNotNull(toCheck.Id);
            Assert.AreEqual("TEST USERNAME", toCheck.Username);
            Assert.AreEqual("test@test.com", toCheck.Email);
            Assert.AreEqual("TEST NAME", toCheck.Name);
        }

        [Test]
        public void GetUserByInvalidUsernameNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _userRepo.GetUserByUsername(null));
        }

        [Test]
        public void GetUserByInvalidUsernameDNETest()
        {
            Assert.Throws<InvalidUsernameException>(() => _userRepo.GetUserByUsername("Test"));
        }

        [Test]
        public void GetRoleByInvalidNameNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _userRepo.GetRoleByName(null));
        }

        [Test]
        public void GetRoleByInvalidNameDNETest()
        {
            Assert.Throws<InvalidNameException>(() => _userRepo.GetRoleByName("Test"));
        }

    }
}
