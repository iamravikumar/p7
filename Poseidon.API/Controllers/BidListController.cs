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
    public class BidListController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public BidListController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/BidLists
        /// <summary>
        /// Gets a list of all BidList entities.
        /// </summary>
        /// <returns>A list of all BidList entities.</returns>
        /// <response code="200">Returns the list of all BidList entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<BidList>>> Get()
        {
            return await _context.BidList.ToListAsync();
        }

        // GET: api/BidLists/5
        /// <summary>
        /// Gets a single BidList entity.
        /// </summary>
        /// <param name="id">The Id of the BidList entity to get.</param>
        /// <returns>The specified BidList entity.</returns>
        /// <response code="200">Returns the BidList entity.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidList>> Get(int id)
        {
            var bidList = await _context.BidList.FindAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return bidList;
        }

        // PUT: api/BidLists/5
        /// <summary>
        /// Updates a BidList eneity.
        /// </summary>
        /// <param name="id">The Id of the BidList entity to update.</param>
        /// <param name="bidList">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, BidList bidList)
        {
            if (id != bidList.Id)
            {
                return BadRequest();
            }

            _context.Entry(bidList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidListExists(id))
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

        // POST: api/BidLists
        /// <summary>
        /// Creates a new BidList entity. 
        /// </summary>
        /// <param name="bidList">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BidList>> Post(BidList bidList)
        {
            _context.BidList.Add(bidList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new {id = bidList.Id}, bidList);
        }

        // DELETE: api/BidLists/5
        /// <summary>
        /// Deletes a specified BidList entity.
        /// </summary>
        /// <param name="id">The Id the BidList entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BidList>> Delete(int id)
        {
            var bidList = await _context.BidList.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            _context.BidList.Remove(bidList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidListExists(int id)
        {
            return _context.BidList.Any(e => e.Id == id);
        }
    }
}