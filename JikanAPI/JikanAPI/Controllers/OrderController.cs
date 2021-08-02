using JikanAPI.Models;
using JikanAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace JikanAPI.Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController : Controller
    {
        IJikanService _service;

        public OrderController(IJikanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds an order.
        /// </summary>
        /// <param name="order">The order to be added.</param>  
        /// <returns>The order that was added.</returns>
        /// <response code="200">Returns the newly added order.</response>
        /// <response code="400">If the order is null</response>
        /// <response code="500">If there is another error.</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddOrder(Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest("Order is null");

                _service.AddOrder(order);
                return Ok(order);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets an order by its id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>The order with the corresponding id.</returns>
        /// <response code="200">Returns the order with the corresponding id.</response>
        /// <response code="500">If there is another error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                return Ok(_service.GetOrderById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all orders for a particular user.
        /// </summary>
        /// <returns>A list of all orders for a particular user. If Admin, returns all orders.</returns>
        /// <response code="200">Returns a list of all orders for a particular user. If Admin, returns all orders.</response>
        /// <response code="500">If there is another error.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllOrders()
        {
            try
            {
                if (User.Claims.Any(c => c.Type == ClaimTypes.Role.ToString() && c.Value == "Admin"))
                {
                    return Ok(_service.GetAllOrders());
                }
                else
                {
                    int curUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    return Ok(_service.GetOrdersByUserId(curUserId));
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes an order by its id.
        /// </summary>
        /// <param name="id">The id of the order to be deleted.</param>  
        /// <returns>Ok status if deleted successfully.</returns>
        /// <response code="200">Returns Ok status if deleted successfully.</response>
        /// <response code="500">If there is another error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _service.DeleteOrder(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
