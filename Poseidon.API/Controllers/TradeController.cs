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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Trade>>> Get()
        {
            return await _context.Trade.ToListAsync();
        }

        // GET: api/Trade/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

            return trade;
        }

        private bool TradeExists(short id)
        {
            return _context.Trade.Any(e => e.Id == id);
        }
    }
}
