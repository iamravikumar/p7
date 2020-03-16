using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.Data;
using Poseidon.API.Models;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TradeController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public TradeController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/Trade
        /// <summary>
        /// Gets a list of all Trade entities.
        /// </summary>
        /// <returns>A list of all Trade entities.</returns>
        /// <response code="200">Returns the list of all Trade entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Trade>>> Get()
        {
            return await _context.Trade.ToListAsync();
        }

        // GET: api/Trade/5
        /// <summary>
        /// Gets a single Trade entity.
        /// </summary>
        /// <param name="id">The Id of the Trade entity to get.</param>
        /// <returns>The specified Trade entity.</returns>
        /// <response code="200">Returns the Trade entity.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Trade>> Get(short id)
        {
            var trade = await _context.Trade.FindAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return trade;
        }

        // PUT: api/Trade/5
        /// <summary>
        /// Updates a Trade eneity.
        /// </summary>
        /// <param name="id">The Id of the Trade entity to update.</param>
        /// <param name="trade">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(short id, Trade trade)
        {
            if (id != trade.Id)
            {
                return BadRequest();
            }

            _context.Entry(trade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trade
        /// <summary>
        /// Creates a new Trade entity. 
        /// </summary>
        /// <param name="trade">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Trade>> Post(Trade trade)
        {
            _context.Trade.Add(trade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = trade.Id }, trade);
        }

        // DELETE: api/Trade/5
        /// <summary>
        /// Deletes a specified Trade entity.
        /// </summary>
        /// <param name="id">The Id the Trade entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Trade>> Delete(short id)
        {
            var trade = await _context.Trade.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            _context.Trade.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeExists(short id)
        {
            return _context.Trade.Any(e => e.Id == id);
        }
    }
}
