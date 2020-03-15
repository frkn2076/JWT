using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Business;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        public IManager manager { get; }

        public ValuesController(IManager manager) {
            this.manager = manager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequestModel model) {
            var token = manager.Authenticate(model.userName, model.password);
            return Ok(token);
        }

        [HttpGet("do")]
        public ActionResult<string> GetHi() {
            return "Heyyyyy!!";
        }
    }
}
