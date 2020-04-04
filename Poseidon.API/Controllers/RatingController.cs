using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.ActionFilters;
using Poseidon.API.Models;
using Poseidon.API.Services.Interfaces;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // GET: api/Ratings
        /// <summary>
        /// Gets a list of all Rating entities.
        /// </summary>
        /// <returns>A list of all Rating entities.</returns>
        /// <response code="200">Returns the list of all Rating entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No Rating entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RatingInputModel>>> Get()
        {
            var results =
                await _ratingService.GetAllRatingsAsInputModelsAsync();

            var entityList =
                results as RatingInputModel[] ?? results.ToArray();

            if (!entityList.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Ratings/5
        /// <summary>
        /// Gets a single Rating entity.
        /// </summary>
        /// <param name="id">The Id of the Rating entity to get.</param>
        /// <returns>The specified Rating entity.</returns>
        /// <response code="200">Returns the Rating entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RatingInputModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!_ratingService.RatingExists(id))
            {
                return NotFound();
            }

            var result = await _ratingService.GetRatingByIdAsInputModelASync(id);

            return Ok(result);
        }

        // POST: api/Ratings
        /// <summary>
        /// Creates a new Rating entity. 
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
        public async Task<ActionResult<RatingInputModel>> Post(RatingInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }

            var resultId = await _ratingService.CreateRating(inputModel);

            return CreatedAtAction("Get", new { id = resultId }, inputModel);
        }

        // PUT: api/Ratings/5
        /// <summary>
        /// Updates a Rating entity.
        /// </summary>
        /// <param name="id">The Id of the Rating entity to update.</param>
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
        public async Task<ActionResult> Put(int id, RatingInputModel inputModel)
        {
            if (!_ratingService.RatingExists(id))
            {
                return NotFound($"No Rating entity matching the id [{id}] was found.");
            }

            await _ratingService.UpdateRating(id, inputModel);

            return NoContent();
        }

        // DELETE: api/Ratings/5
        /// <summary>
        /// Deletes a specified Rating entity.
        /// </summary>
        /// <param name="id">The Id the Rating entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="403">The user lacks privileges to access resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rating>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    $"The '{nameof(id)}' argument must a non-zero, positive integer value. The passed-in value was {id}");
            }

            if (!_ratingService.RatingExists(id))
            {
                return NotFound($"No {typeof(Rating)} entity matching the id [{id}] was found.");
            }

            await _ratingService.DeleteRating(id);

            return NoContent();
        }
    }
}