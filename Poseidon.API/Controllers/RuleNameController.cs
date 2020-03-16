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
    public class RuleNameController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public RuleNameController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/RuleName
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetRuleName()
        {
            return await _context.RuleName.ToListAsync();
        }

        // GET: api/RuleName/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleName>> GetRuleName(short id)
        {
            var ruleName = await _context.RuleName.FindAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return ruleName;
        }

        // PUT: api/RuleName/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleName(short id, RuleName ruleName)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RuleName>> PostRuleName(RuleName ruleName)
        {
            _context.RuleName.Add(ruleName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleName", new { id = ruleName.Id }, ruleName);
        }

        // DELETE: api/RuleName/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RuleName>> DeleteRuleName(short id)
        {
            var ruleName = await _context.RuleName.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }

            _context.RuleName.Remove(ruleName);
            await _context.SaveChangesAsync();

            return ruleName;
        }

        private bool RuleNameExists(short id)
        {
            return _context.RuleName.Any(e => e.Id == id);
        }
    }
}
