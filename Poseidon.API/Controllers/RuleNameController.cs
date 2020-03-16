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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<RuleName>>> Get()
        {
            return await _context.RuleName.ToListAsync();
        }

        // GET: api/RuleName/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

            return ruleName;
        }

        private bool RuleNameExists(short id)
        {
            return _context.RuleName.Any(e => e.Id == id);
        }
    }
}
