using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    public class AuthenticationController : Controller
    {

        // POST api/values
        [HttpPost]
        [Route("api/auth/authenticate", Name = "Authenticate")]
        public IActionResult Post([FromBody]AuthenticationRequest request)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(new AuthenticationResponse()
            {
                IsAuthorized = true
            });
        }

        [HttpPost]
        [Route("api/auth/login", Name = "Login")]
        public IActionResult Login(string username, string password)
        {
            if("test" == username && "test" == password)
            {
                return Ok(new AuthenticationResponse() { IsAuthorized = true});
            }
            return BadRequest("Invalid credentials provided");
        }
    }
}
