using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Communication.Requests.Login;

namespace TatooMarket.Api.Controllers
{
    public class LoginController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] RequestLogin request)
    }
}
