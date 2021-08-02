using System;
using JikanAPI.Models;
using JikanAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JikanAPI.Controllers
{
    [ApiController]
    [Route("/api/watch")]
    [Authorize(Roles = "Admin")]
    public class WatchController : Controller
    {
        IJikanService _service;
        public WatchController(IJikanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a watch.
        /// </summary>
        /// <param name="toAdd">The watch to be added</param>  
        /// <returns>The watch that was added.</returns>
        /// <response code="200">Returns the newly added watch.</response>
        /// <response code="400">If the watch is null</response>
        /// <response code="500">If there is another error.</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddWatch(Watch toAdd)
        {
            try
            {
                if (toAdd == null)
                    return BadRequest("Watch is null.");

                _service.AddWatch(toAdd);
                return Ok(toAdd);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a watch by its id.
        /// </summary>
        /// <param name="id">The id of the watch to be searched.</param>  
        /// <returns>The watch with the corresponding id.</returns>
        /// <response code="200">Returns the watch with the corresponding id.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchById(int id)
        {
            try
            {
                return Ok(_service.GetWatchById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a watch by its name.
        /// </summary>
        /// <param name="name">The name of the watch to be searched.</param>  
        /// <returns>The watch with the corresponding name.</returns>
        /// <response code="200">Returns the watch with the corresponding name.</response>
        /// <response code="400">If the name is null</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchByName(string name)
        {
            try
            {
                if (name == null)
                    return BadRequest("Name is null.");

                return Ok(_service.GetWatchByName(name));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all watches.
        /// </summary>
        /// <returns>A list of all watches.</returns>
        /// <response code="200">Returns a list of all watches.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllWatches()
        {
            try
            {
                return Ok(_service.GetAllWatches());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a list of watches of a particular type.
        /// </summary>
        /// <param name="type">The type of the watches to be searched.</param>  
        /// <returns>A list of watches with the corresponding type.</returns>
        /// <response code="200">Returns a list of watches with the corresponding type.</response>
        /// <response code="400">If the type is null</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("type/{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchesByType(string type)
        {
            try
            {
                if (type == null)
                    return BadRequest("Type is null.");

                return Ok(_service.GetWatchesByType(type));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a watch by its maximum price.
        /// </summary>
        /// <param name="max">The maximum price of the watch to be searched.</param>  
        /// <returns>A list of watches with a price less than or equal to the maximum price.</returns>
        /// <response code="200">Returns a list of watches with a price less than or equal to the maximum price.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("price/{max}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchesByPrice(decimal max)
        {
            try
            {
                return Ok(_service.GetWatchesByPrice(max));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Edits a particular watch.
        /// </summary>
        /// <param name="watch">The watch to be edited.</param>  
        /// <returns>The newly edited watch.</returns>
        /// <response code="200">Returns the newly edited watch.</response>
        /// <response code="400">If the watch is null</response>
        /// <response code="500">If there is another error.</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditWatch(Watch watch)
        {
            try
            {
                if (watch == null)
                    return BadRequest("Watch is null.");

                _service.EditWatch(watch);
                return Ok(watch);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a watch by its id.
        /// </summary>
        /// <param name="id">The id of the watch to be deleted.</param>  
        /// <returns>Ok status</returns>
        /// <response code="200">Returns if the watch is successfully deleted.</response>
        /// <response code="500">If there is another error.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteWatch(int id)
        {
            try
            {
                _service.DeleteWatch(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all watches of a particular order by an order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A list of watches with the associated order id.</returns>
        /// <response code="200">Returns a list of watches with the associated order id.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("order/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchesByOrderId(int id)
        {
            try
            {
                return Ok(_service.GetWatchesByOrderId(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets the quantity of a watch by an order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A list of watch quantities with the associated order id.</returns>
        /// <response code="200">Returns a list of watch quantities with the associated order id.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("order/quantity/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchQuantityByOrderId(int id)
        {
            try
            {
                return Ok(_service.GetWatchQuantityByOrderId(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets watch with its corresponding quantity by its order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A dictionary of each watch with its corresponding quantity with the associated order id.</returns>
        /// <response code="200">Returns a dictionary of each watch with its corresponding quantity with the associated order id.</response>
        /// <response code="500">If there is another error.</response> 
        [AllowAnonymous]
        [HttpGet("order/watch/quantity/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWatchQuantityPair(int id)
        {
            try
            {
                return Ok(_service.GetWatchQuantityPair(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
