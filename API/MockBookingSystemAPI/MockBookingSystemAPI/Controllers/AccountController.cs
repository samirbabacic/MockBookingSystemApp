using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystemAPI.Requests.Account;
using MockBookingSystemAPI.Responses.Account;

namespace MockBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<AccountRegisterResponse> Register(AccountRegisterRequest request)
        {
            var tokenOutput = _accountService.Register(request);

            return new AccountRegisterResponse() { Token = tokenOutput.Token };
        }
    }
}
