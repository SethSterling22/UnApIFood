using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;

namespace UnApIFood.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginService _loginService;
        public LoginController(
            LoginService loginService
        )
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> LoginAsync([FromBody] Login login)
        {
            try
            {
                return Ok(await _loginService.LoginAsync(login));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}