using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poseidon.API.ActionFilters;
using Poseidon.API.Models;
using Poseidon.API.Services.Interfaces;

namespace Poseidon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurvePointController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        // GET: api/CurvePoints
        /// <summary>
        /// Gets a list of all CurvePoint entities.
        /// </summary>
        /// <returns>A list of all CurvePoint entities.</returns>
        /// <response code="200">Returns the list of all CurvePoint entities.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">No CurvePoint entities were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CurvePointViewModel>>> Get()
        {
            var results = 
                await _curvePointService.GetAllCurvePointsAsViewModelsAsync();

            var entityList = 
                results as CurvePointViewModel[] ?? results.ToArray();

            if (!entityList.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/CurvePoints/5
        /// <summary>
        /// Gets a single CurvePoint entity.
        /// </summary>
        /// <param name="id">The Id of the CurvePoint entity to get.</param>
        /// <returns>The specified CurvePoint entity.</returns>
        /// <response code="200">Returns the CurvePoint entity.</response>
        /// <response code="400">Bad Id.</response>
        /// <response code="404">The specified entity was not found.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CurvePointViewModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!_curvePointService.CurvePointExists(id))
            {
                return NotFound();
            }

            var result = await _curvePointService.GetCurvePointByIdAsViewModelASync(id);

            return Ok(result);
        }

        // POST: api/CurvePoints
        /// <summary>
        /// Creates a new CurvePoint entity. 
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
        public async Task<ActionResult<CurvePointInputModel>> Post(CurvePointInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var resultId = await _curvePointService.CreateCurvePoint(inputModel);

                return CreatedAtAction("Get", new { id = resultId }, inputModel);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/CurvePoints/5
        /// <summary>
        /// Updates a CurvePoint entity.
        /// </summary>
        /// <param name="id">The Id of the CurvePoint entity to update.</param>
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
        public async Task<ActionResult> Put(int id, CurvePointInputModel inputModel)
        {
            if (id != inputModel.Id)
            {
                return BadRequest("Id mismatch");
            }

            if (!_curvePointService.CurvePointExists(id))
            {
                return NotFound($"No CurvePoint enti" +
                                $"ty matching the id [{id}] was found.");
            }

            await _curvePointService.UpdateCurvePoint(id, inputModel);

            return NoContent();
        }

        // DELETE: api/CurvePoints/5
        /// <summary>
        /// Deletes a specified CurvePoint entity.
        /// </summary>
        /// <param name="id">The Id the CurvePoint entity to delete.</param>
        /// <returns>Null.</returns>
        /// <response code="204">The entity was successfully created.</response>
        /// <response code="401">The user is not authorized to access this resource.</response>
        /// <response code="404">The specified entity was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CurvePoint>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    $"The '{nameof(id)}' argument must a non-zero, positive integer value. The passed-in value was {id}");
            }

            if (!_curvePointService.CurvePointExists(id))
            {
                return NotFound($"No {typeof(CurvePoint)} entity matching the id [{id}] was found.");
            }

            await _curvePointService.DeleteCurvePoint(id);

            return NoContent();
        }
    }
}
