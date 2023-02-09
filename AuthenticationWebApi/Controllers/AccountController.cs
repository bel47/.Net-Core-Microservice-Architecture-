using JwtAutenticationManager;
using JwtAutenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _tokenHandler;

        public AccountController(JwtTokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authemticate([FromBody] AuthenticationRequest request) { 
            var authenticationResponse = _tokenHandler.GenrateJwtToken(request);
            if(authenticationResponse == null) return Unauthorized();
            return Ok(authenticationResponse);
        
        }
    }
}
