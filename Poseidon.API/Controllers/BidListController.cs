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
    public class BidListController : ControllerBase
    {
        private readonly PoseidonContext _context;

        public BidListController(PoseidonContext context)
        {
            _context = context;
        }

        // GET: api/BidLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetBidList()
        {
            return await _context.BidList.ToListAsync();
        }

        // GET: api/BidLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidList>> GetBidList(int id)
        {
            var bidList = await _context.BidList.FindAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return bidList;
        }

        // PUT: api/BidLists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidList(int id, BidList bidList)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BidList>> PostBidList(BidList bidList)
        {
            _context.BidList.Add(bidList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidList", new { id = bidList.Id }, bidList);
        }

        // DELETE: api/BidLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BidList>> DeleteBidList(int id)
        {
            var bidList = await _context.BidList.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            _context.BidList.Remove(bidList);
            await _context.SaveChangesAsync();

            return bidList;
        }

        private bool BidListExists(int id)
        {
            return _context.BidList.Any(e => e.Id == id);
        }
    }
}
