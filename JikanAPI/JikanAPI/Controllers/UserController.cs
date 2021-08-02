using JikanAPI.Models.ViewModels.Requests;
using JikanAPI.Repos;
using JikanAPI.Service;
using JikanAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Controllers
{
    [ApiController]
    [Route("/api/user")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        IJikanService _service;

        public UserController(IJikanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Attempts to register a user with the provided user information.
        /// </summary>
        /// <param name="vm">A RegisterUserViewModel representing a username, email, name, and password.</param>  
        /// <returns>A boolean indicating successful registration.</returns>
        /// <response code="200">True if regsitered</response>
        /// <response code="400">If the user is null</response>
        /// <response code="500">If there is another error.</response>  
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterUser(RegisterUserViewModel vm)
        {
            try
            {
                if (vm == null)
                    return BadRequest("User is null.");

                _service.RegisterUser(vm);
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Attempts to login a user with given credentials.
        /// </summary>
        /// <param name="vm">A LoginViewModel representing a username and password.</param>  
        /// <returns>An anonymous type containing a username and its associated token.</returns>
        /// <response code="200">Returns the username with the authentication token. </response>
        /// <response code="400">If the login credentials are null</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(LoginViewModel vm)
        {
            try
            {
                if (vm == null)
                    return BadRequest("Login Request in null.");

                string token = _service.Login(vm);
                return Ok(new { vm.Username, token });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all users. Requires Admin access.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <response code="200">Returns a list of all users.</response>
        /// <response code="500">If there is another error.</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_service.GetAllUsers());
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a user by id. Requires Admin Access.
        /// </summary>
        /// <response code="200">If successfully deleted.</response>
        /// <response code="500">If there is another error.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _service.DeleteUser(id);
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
