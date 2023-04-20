using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using practices.Models;
using practices.Data;
using practices.Repository;

namespace practices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountRepository _accountRepository;



        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var result = await _accountRepository.Signup(signupModel);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return BadRequest();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninModel signinModel)
        {
            var result = await _accountRepository.Login(signinModel);

            return Ok(result);
        }


    }

}