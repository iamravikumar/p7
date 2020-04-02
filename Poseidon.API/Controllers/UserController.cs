using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.ActionFilters;
using Poseidon.API.Models;
using Poseidon.API.Services;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        /// <summary>
        /// Gets a list of all User entities.
        /// </summary>
        /// <returns>A list of all User entities.</returns>
        /// <response code="200">Returns the list of all User entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No User entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserInputModel>>> Get()
        {
            var results =
                await _userService.GetAllUsersAsInputModelsAsync();

            var entityList =
                results as UserInputModel[] ?? results.ToArray();

            if (!entityList.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Users/5
        /// <summary>
        /// Gets a single User entity.
        /// </summary>
        /// <param name="id">The Id of the User entity to get.</param>
        /// <returns>The specified User entity.</returns>
        /// <response code="200">Returns the User entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInputModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            var result = await _userService.GetUserByIdAsInputModelASync(id);

            return Ok(result);
        }

        // POST: api/Users
        /// <summary>
        /// Creates a new User entity. 
        /// </summary>
        /// <param name="inputModel">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserInputModel>> Post(UserInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }

            var resultId = await _userService.CreateUser(inputModel);

            return CreatedAtAction("Get", new { id = resultId }, inputModel);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Updates a User entity.
        /// </summary>
        /// <param name="id">The Id of the User entity to update.</param>
        /// <param name="inputModel">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, UserInputModel inputModel)
        {
            if (!_userService.UserExists(id))
            {
                return NotFound($"No User entity matching the id [{id}] was found.");
            }

            await _userService.UpdateUser(id, inputModel);

            return NoContent();
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Deletes a specified User entity.
        /// </summary>
        /// <param name="id">The Id the User entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="403">The user lacks privileges to access resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    $"The '{nameof(id)}' argument must a non-zero, positive integer value. The passed-in value was {id}");
            }

            if (!_userService.UserExists(id))
            {
                return NotFound($"No {typeof(User)} entity matching the id [{id}] was found.");
            }

            await _userService.DeleteUser(id);

            return NoContent();
        }
    }
}
