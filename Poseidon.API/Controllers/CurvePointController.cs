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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> Get()
        {
            return await _context.CurvePoint.ToListAsync();
        }

        // GET: api/CurvePoints/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

            return curvePoint;
        }

        private bool CurvePointExists(short id)
        {
            return _context.CurvePoint.Any(e => e.Id == id);
        }
    }
}
