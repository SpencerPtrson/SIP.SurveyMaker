using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIP.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        // POST api/<ActivationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
