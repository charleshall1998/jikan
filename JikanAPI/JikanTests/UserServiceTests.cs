using JikanAPI.Exceptions;
using JikanAPI.Models.Auth;
using JikanAPI.Models.ViewModels.Requests;
using JikanAPI.Repos.InMem;
using JikanAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanTests
{
    class UserServiceTests
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
        public void RegisterUserSameUsernameTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME",
                Password = "12345"
            };

            _service.RegisterUser(toRegister);

            Assert.Throws<InvalidUsernameException>(() => _service.RegisterUser(toRegister));
        }

        [Test]
        public void RegisterUserNullUsernameTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = null,
                Email = "test@test.com",
                Name = "TEST NAME",
                Password = "12345"
            };

            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toRegister));
        }

        [Test]
        public void RegisterUserNullEmailTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = "TEST USERNAME",
                Email = null,
                Name = "TEST NAME",
                Password = "12345"
            };

            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toRegister));
        }

        [Test]
        public void RegisterUserNullNameTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = null,
                Password = "12345"
            };

            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toRegister));
        }

        [Test]
        public void RegisterUserNullPasswordTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME",
                Password = null
            };

            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toRegister));
        }

        [Test]
        public void LoginNullUsernameTest()
        {
            LoginViewModel toLogin = new LoginViewModel
            {
                Username = null,
                Password = "12345"
            };

            Assert.Throws<ArgumentNullException>(() => _service.Login(toLogin));
        }

        [Test]
        public void LoginNullPasswordTest()
        {
            LoginViewModel toLogin = new LoginViewModel
            {
                Username = "TEST USERNAME",
                Password = null
            };

            Assert.Throws<ArgumentNullException>(() => _service.Login(toLogin));
        }

        [Test]
        public void LoginInvalidPasswordTest()
        {
            RegisterUserViewModel toRegister = new RegisterUserViewModel
            {
                Username = "TEST USERNAME",
                Email = "test@test.com",
                Name = "TEST NAME",
                Password = "12345"
            };

            _service.RegisterUser(toRegister);

            LoginViewModel toLogin = new LoginViewModel
            {
                Username = "TEST USERNAME",
                Password = "54321"
            };

            Assert.Throws<InvalidPasswordException>(() => _service.Login(toLogin));
        }
    }
}
