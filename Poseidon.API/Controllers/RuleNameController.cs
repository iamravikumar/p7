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
    public class RuleNameController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public RuleNameController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/RuleName
        /// <summary>
        /// Gets a list of all RuleName entities.
        /// </summary>
        /// <returns>A list of all RuleName entities.</returns>
        /// <response code="200">Returns the list of all RuleName entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<RuleName>>> Get()
        {
            return await _context.RuleName.ToListAsync();
        }

        // GET: api/RuleName/5
        /// <summary>
        /// Gets a single RuleName entity.
        /// </summary>
        /// <param name="id">The Id of the RuleName entity to get.</param>
        /// <returns>The specified RuleName entity.</returns>
        /// <response code="200">Returns the RuleName entity.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RuleName>> Get(short id)
        {
            var ruleName = await _context.RuleName.FindAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return ruleName;
        }

        // PUT: api/RuleName/5
        /// <summary>
        /// Updates a RuleName eneity.
        /// </summary>
        /// <param name="id">The Id of the RuleName entity to update.</param>
        /// <param name="ruleName">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(short id, RuleName ruleName)
        {
            if (id != ruleName.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruleName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleNameExists(id))
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

        // POST: api/RuleName
        /// <summary>
        /// Creates a new RuleName entity. 
        /// </summary>
        /// <param name="ruleName">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RuleName>> Post(RuleName ruleName)
        {
            _context.RuleName.Add(ruleName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = ruleName.Id }, ruleName);
        }

        // DELETE: api/RuleName/5
        /// <summary>
        /// Deletes a specified RuleName entity.
        /// </summary>
        /// <param name="id">The Id the RuleName entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RuleName>> Delete(short id)
        {
            var ruleName = await _context.RuleName.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }

            _context.RuleName.Remove(ruleName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleNameExists(short id)
        {
            return _context.RuleName.Any(e => e.Id == id);
        }
    }
}
