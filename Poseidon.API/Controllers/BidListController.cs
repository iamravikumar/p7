using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.ActionFilters;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Services.Interfaces;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidListController : ControllerBase
    {
        private readonly PoseidonContext _context;
        private readonly IBidListService _bidListService;
        private readonly IMapper _mapper;

        public BidListController(IBidListService bidListService, PoseidonContext context, IMapper mapper)
        {
            _bidListService = bidListService;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BidLists
        /// <summary>
        /// Gets a list of all BidList entities.
        /// </summary>
        /// <returns>A list of all BidList entities.</returns>
        /// <response code="200">Returns the list of all BidList entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No BidList entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<BidList>>> Get()
        {
            var result = await _bidListService.GetAllBidListsAsync();

            var bidLists = result as BidList[] ?? result.ToArray();
            
            if (bidLists.ToList().Count > 0)
            {
                var returnResults = _mapper.Map<IEnumerable<BidList>, IEnumerable<BidListViewModel>>(bidLists);

                return Ok(returnResults);
            }

            return NotFound();
        }

        // GET: api/BidLists/5
        /// <summary>
        /// Gets a single BidList entity.
        /// </summary>
        /// <param name="id">The Id of the BidList entity to get.</param>
        /// <returns>The specified BidList entity.</returns>
        /// <response code="200">Returns the BidList entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidList>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var bidList = await _bidListService.GetBidListByIdAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return Ok(bidList);
        }


        // POST: api/BidLists
        /// <summary>
        /// Creates a new BidList entity. 
        /// </summary>
        /// <param name="inputModel">Data for the new entity.</param>
        /// <returns>The created entity.</returns>
        /// <response code="201">The entity was successfully created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BidList>> Post(BidListInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<BidList>(inputModel);

                await _bidListService.CreateBidList(entity);

                return CreatedAtAction("Get", new {id = entity.Id}, inputModel);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/BidLists/5
        /// <summary>
        /// Updates a BidList entity.
        /// </summary>
        /// <param name="id">The Id of the BidList entity to update.</param>
        /// <param name="inputModel">Updated data.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The resource was successfully updated.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, BidListInputModel inputModel)
        {
            if (id <= 0)
            {
                return BadRequest("The 'Id' argument must be >= 1.");
            }

            var entity = await _bidListService.GetBidListByIdAsync(id);

            if (entity == null)
            {
                return NotFound("No entity matching the supplied id was found.");
            }

            _mapper.Map(inputModel, entity);

            await _bidListService.UpdateBidList(entity);

            return NoContent();
        }

        // DELETE: api/BidLists/5
        /// <summary>
        /// Deletes a specified BidList entity.
        /// </summary>
        /// <param name="id">The Id the BidList entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidList>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("The 'Id' argument must be >= 1.");
            }

            var bidList = await _bidListService.GetBidListByIdAsync(id);
            
            if (bidList == null)
            {
                return NotFound("No entity matching the supplied id was found.");
            }

            await _bidListService.DeleteBidList(bidList);

            return NoContent();
        }

        private bool BidListExists(int id)
        {
            return _context.BidList.Any(e => e.Id == id);
        }
    }
}