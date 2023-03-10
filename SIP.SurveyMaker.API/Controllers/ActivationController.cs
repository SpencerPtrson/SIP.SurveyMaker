using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIP.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
        // GET: api/<ActivationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<ActivationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActivationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
