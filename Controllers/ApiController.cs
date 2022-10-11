using Accepted_Assignment.Helpers;
using Accepted_Assignment.Models;
using Accepted_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accepted_Assignment.Controllers
{
	[ApiController]
	[Route("api")]
	public class ApiController : ControllerBase
	{

		private readonly ILogger<ApiController> _logger;
		private readonly IMatchRepository _matchRepository;
		private readonly IMatchOddsRepository _matchOddsRepository;
		public ApiController(ILogger<ApiController> logger, IMatchRepository matchRepository, IMatchOddsRepository matchOddsRepository)
		{
			_logger = logger;
			_matchRepository = matchRepository;
			_matchOddsRepository = matchOddsRepository;
		}


		/// <summary>
		/// Upsert Match.
		/// </summary>
		/// <returns> Match</returns>
		/// <remarks>
		/// Sample request:
		///
		///     POST Match/Upsert
		///     {
		///        "description": "OSFP - PAO",
		///        "matchDate": "2022-10-10T20:12",
		///        "teamA": "OSFP",
		///        "teamB": "PAO",
		///        "sport" : 2
		///     }
		///
		/// </remarks>
		/// <response code="200">Returns the new match</response>
		/// <response code="400">Error occured in creation</response>
		[HttpPost]
		[Route("Match/[action]")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Upsert(MatchModel model)
		{
			try
			{
				MatchModel newModel = await _matchRepository.Upsert(model);
				return Ok(newModel);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Query Matches.
		/// </summary>
		/// <returns>List of matches</returns>
		/// <remarks>
		/// Sample request:
		///
		///     POST Match/Query
		///     {
		///		   "like":"pa",
		///        "offset": 0,
		///        "limit": 10
		///     }
		///
		/// </remarks>
		/// <response code="200">Returns a list of matches</response>
		/// <response code="400">Error occured in querying</response>
		[HttpPost]
		[Route("Match/[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Query(Lookup lookup)
		{
			try
			{
				List<MatchModel> models = await _matchRepository.Query(lookup);
				return Ok(models);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Get a single Match.
		/// </summary>
		/// <returns>Match</returns>
		/// <param name = "id" ></param>
		/// <response code="200">Returns a single match</response>
		/// <response code="400">Error occured in querying</response>
		/// <response code="404">Match wasn't found</response>
		[HttpGet]
		[Route("Match/[action]/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetSingle(int id)
		{
			try
			{
				MatchModel model = await _matchRepository.GetSingle(id);
				return Ok(model);
			}
			catch(NullReferenceException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Delete a single Match.
		/// </summary>
		/// <param name = "id" ></param>
		/// <response code="200">Deletes a single match</response>
		/// <response code="400">Error occured in deletion</response>
		/// <response code="404">Match wasn't found</response>
		[HttpDelete]
		[Route("Match/[action]/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _matchRepository.Delete(id);
				return Ok();
			}
			catch (NullReferenceException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Upsert Match odds.
		/// </summary>
		/// <returns>Match odds</returns>
		/// <remarks>
		/// Sample request:
		///
		///     POST MatchOdds/Upsert
		///     {
		///        "matchId": 2,
		///        "specifier": "x",
		///        "odd": 2.5
		///     }
		///
		/// </remarks>
		/// <response code="200">Returns the new match odd</response>
		/// <response code="400">Error occured in creation</response>
		/// <response code="404">The associated Match wasn't found</response>
		[HttpPost]
		[Route("MatchOdds/[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Upsert(MatchOddsModel model)
		{
			try
			{
				MatchOddsModel newModel = await _matchOddsRepository.Upsert(model);
				return Ok(newModel);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (NullReferenceException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Query Match odds.
		/// </summary>
		/// <returns>List of match odds</returns>
		/// <remarks>
		/// Sample request:
		///
		///     POST MatchOdds/Query
		///     {
		///        "offset": 0,
		///        "limit": 10
		///     }
		///
		/// </remarks>
		/// <response code="200">Returns a list of match odds</response>
		/// <response code="400">Error occured in querying</response>
		[HttpPost]
		[Route("MatchOdds/Query")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> QueryMatchOdd(Lookup lookup)
		{
			try
			{
				List<MatchOddsModel> models = await _matchOddsRepository.Query(lookup);
				return Ok(models);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Get a single Match odds.
		/// </summary>
		/// <returns>Match Odd</returns>
		/// <param name = "id" ></param>
		/// <response code="200">Returns a single match odds</response>
		/// <response code="400">Error occured in querying</response>
		/// <response code="404">Match odd wasn't found</response>

		[HttpGet]
		[Route("MatchOdds/GetSingle/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetSingleMatchOdd(int id)
		{
			try
			{
				MatchOddsModel model = await _matchOddsRepository.GetSingle(id);
				return Ok(model);
			}
			catch (NullReferenceException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		/// <summary>
		/// Delete a single Match Odds.
		/// </summary>
		/// <param name = "id" ></param>
		/// <response code="200">Deletes a single match Odds</response>
		/// <response code="400">Error occured in deletion</response>
		/// <response code="404">Match Odds wasn't found</response>
		[HttpDelete]
		[Route("MatchOdds/Delete/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteMatchOdd(int id)
		{
			try
			{
				await _matchOddsRepository.Delete(id);
				return Ok();
			}
			catch (NullReferenceException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
