using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace DadJokeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RandomJokeController : ControllerBase
    {
        
        private readonly IApiManager _apiManager;

        public RandomJokeController( IApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        [HttpGet("getRandomJoke")]
        public async Task<IActionResult> GetRandomJoke()
        {
            try
            {
                var response = await _apiManager.GetRandomJokeAsync();
                if(response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                var response = "Error processing the request";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet("getRandomJokes")]
        public async Task<IActionResult> GetRandomJokes(int count)
        {
            try
            {
                var jokes = await _apiManager.GetRandomJokesAsync(count);
                if (jokes.Length == 0)
                {
                    return NotFound();
                }
                return Ok(jokes);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var response = "Error processing the request";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("getJokeCount")]
        public async Task<IActionResult> GetJokeCount()
        {
            try
            {
                var response = await _apiManager.GetJokeCountAsync();
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                var errorResponse = "Error processing the request";
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

    }
}
