using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetCurvePoint()
        {
            return await _context.CurvePoint.ToListAsync();
        }

        // GET: api/CurvePoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePoint>> GetCurvePoint(short id)
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
        public async Task<IActionResult> PutCurvePoint(short id, CurvePoint curvePoint)
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
        public async Task<ActionResult<CurvePoint>> PostCurvePoint(CurvePoint curvePoint)
        {
            _context.CurvePoint.Add(curvePoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurvePoint", new { id = curvePoint.Id }, curvePoint);
        }

        // DELETE: api/CurvePoints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CurvePoint>> DeleteCurvePoint(short id)
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
