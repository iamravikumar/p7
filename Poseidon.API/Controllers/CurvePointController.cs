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
    public class CurvePointController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public CurvePointController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/CurvePoints
        /// <summary>
        /// Gets a list of all CurvePoint entities.
        /// </summary>
        /// <returns>A list of all CurvePoint entities.</returns>
        /// <response code="200">Returns the list of all CurvePoint entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> Get()
        {
            return await _context.CurvePoint.ToListAsync();
        }

        // GET: api/CurvePoints/5
        /// <summary>
        /// Gets a single CurvePoint entity.
        /// </summary>
        /// <param name="id">The Id of the CurvePoint entity to get.</param>
        /// <returns>The specified CurvePoint entity.</returns>
        /// <response code="200">Returns the CurvePoint entity.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurvePoint>> Get(short id)
        {
            var curvePoint = await _context.CurvePoint.FindAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return curvePoint;
        }

        // PUT: api/CurvePoints/5
        /// <summary>
        /// Updates a CurvePoint eneity.
        /// </summary>
        /// <param name="id">The Id of the CurvePoint entity to update.</param>
        /// <param name="curvePoint">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(short id, CurvePoint curvePoint)
        {
            if (id != curvePoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(curvePoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurvePointExists(id))
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

        // POST: api/CurvePoints
        /// <summary>
        /// Creates a new CurvePoint entity. 
        /// </summary>
        /// <param name="curvePoint">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurvePoint>> Post(CurvePoint curvePoint)
        {
            _context.CurvePoint.Add(curvePoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = curvePoint.Id }, curvePoint);
        }

        // DELETE: api/CurvePoints/5
        /// <summary>
        /// Deletes a specified CurvePoint entity.
        /// </summary>
        /// <param name="id">The Id the CurvePoint entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurvePoint>> Delete(short id)
        {
            var curvePoint = await _context.CurvePoint.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            _context.CurvePoint.Remove(curvePoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurvePointExists(short id)
        {
            return _context.CurvePoint.Any(e => e.Id == id);
        }
    }
}
