using Microsoft.AspNetCore.Mvc;
using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIP.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        // POST api/<ResponseController>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] Response response, bool rollback = false)
        {
            try
            {
                await ResponseManager.Insert(response, rollback);
                return Ok(response.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
