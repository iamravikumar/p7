using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poseidon.API.ActionFilters;
using Poseidon.API.Data;
using Poseidon.API.Models;
using Poseidon.API.Services.Interfaces;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameService _RuleNameService;

        public RuleNameController(IRuleNameService RuleNameService)
        {
            _RuleNameService = RuleNameService;
        }

        // GET: api/RuleNames
        /// <summary>
        /// Gets a list of all RuleName entities.
        /// </summary>
        /// <returns>A list of all RuleName entities.</returns>
        /// <response code="200">Returns the list of all RuleName entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No RuleName entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RuleNameViewModel>>> Get()
        {
            var results =
                await _RuleNameService.GetAllRuleNamesAsViewModelsAsync();

            var entityList =
                results as RuleNameViewModel[] ?? results.ToArray();

            if (!entityList.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/RuleNames/5
        /// <summary>
        /// Gets a single RuleName entity.
        /// </summary>
        /// <param name="id">The Id of the RuleName entity to get.</param>
        /// <returns>The specified RuleName entity.</returns>
        /// <response code="200">Returns the RuleName entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RuleNameViewModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!_RuleNameService.RuleNameExists(id))
            {
                return NotFound();
            }

            var result = await _RuleNameService.GetRuleNameByIdAsViewModelASync(id);

            return Ok(result);
        }

        // POST: api/RuleNames
        /// <summary>
        /// Creates a new RuleName entity. 
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
        public async Task<ActionResult<RuleNameInputModel>> Post(RuleNameInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }

            var resultId = await _RuleNameService.CreateRuleName(inputModel);

            return CreatedAtAction("Get", new { id = resultId }, inputModel);
        }

        // PUT: api/RuleNames/5
        /// <summary>
        /// Updates a RuleName entity.
        /// </summary>
        /// <param name="id">The Id of the RuleName entity to update.</param>
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
        public async Task<ActionResult> Put(int id, RuleNameInputModel inputModel)
        {
            if (!_RuleNameService.RuleNameExists(id))
            {
                return NotFound($"No RuleName enti" +
                                $"ty matching the id [{id}] was found.");
            }

            await _RuleNameService.UpdateRuleName(id, inputModel);

            return NoContent();
        }

        // DELETE: api/RuleNames/5
        /// <summary>
        /// Deletes a specified RuleName entity.
        /// </summary>
        /// <param name="id">The Id the RuleName entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RuleName>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    $"The '{nameof(id)}' argument must a non-zero, positive integer value. The passed-in value was {id}");
            }

            if (!_RuleNameService.RuleNameExists(id))
            {
                return NotFound($"No {typeof(RuleName)} entity matching the id [{id}] was found.");
            }

            await _RuleNameService.DeleteRuleName(id);

            return NoContent();
        }
    }
}