using Microsoft.AspNetCore.Mvc;
using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIP.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
        // GET: api/<ActivationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activation>>> Get()
        {
            try
            {
                return Ok(await ActivationManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<ActivationController>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("About to try ActivationManager.Insert");
                await ActivationManager.Insert(activation, rollback);
                return Ok(activation.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<ActivationController>/5
        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                return Ok(await ActivationManager.Update(activation, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
