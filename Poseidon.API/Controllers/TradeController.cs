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
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // GET: api/Trades
        /// <summary>
        /// Gets a list of all Trade entities.
        /// </summary>
        /// <returns>A list of all Trade entities.</returns>
        /// <response code="200">Returns the list of all Trade entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No Trade entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TradeInputModel>>> Get()
        {
            var results =
                await _tradeService.GetAllTradesAsInputModelsAsync();

            var entityList =
                results as TradeInputModel[] ?? results.ToArray();

            if (!entityList.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Trades/5
        /// <summary>
        /// Gets a single Trade entity.
        /// </summary>
        /// <param name="id">The Id of the Trade entity to get.</param>
        /// <returns>The specified Trade entity.</returns>
        /// <response code="200">Returns the Trade entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TradeInputModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!_tradeService.TradeExists(id))
            {
                return NotFound();
            }

            var result = await _tradeService.GetTradeByIdAsInputModelASync(id);

            return Ok(result);
        }

        // POST: api/Trades
        /// <summary>
        /// Creates a new Trade entity. 
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
        public async Task<ActionResult<TradeInputModel>> Post(TradeInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }

            var resultId = await _tradeService.CreateTrade(inputModel);

            return CreatedAtAction("Get", new { id = resultId }, inputModel);
        }

        // PUT: api/Trades/5
        /// <summary>
        /// Updates a Trade entity.
        /// </summary>
        /// <param name="id">The Id of the Trade entity to update.</param>
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
        public async Task<ActionResult> Put(int id, TradeInputModel inputModel)
        {
            if (!_tradeService.TradeExists(id))
            {
                return NotFound($"No Trade entity matching the id [{id}] was found.");
            }

            await _tradeService.UpdateTrade(id, inputModel);

            return NoContent();
        }

        // DELETE: api/Trades/5
        /// <summary>
        /// Deletes a specified Trade entity.
        /// </summary>
        /// <param name="id">The Id the Trade entity to delete.</param>
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
        public async Task<ActionResult<Trade>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    $"The '{nameof(id)}' argument must a non-zero, positive integer value. The passed-in value was {id}");
            }

            if (!_tradeService.TradeExists(id))
            {
                return NotFound($"No {typeof(Trade)} entity matching the id [{id}] was found.");
            }

            await _tradeService.DeleteTrade(id);

            return NoContent();
        }
    }
}
