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

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> GetJokeCount(int count)
        {
            try
            {
                var response = await _apiManager.GetRandomJokesAsync(count);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                var response = "Error processing the request";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


    }
}