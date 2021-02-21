using MailingApi.Interfaces;
using MailingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserAuthenticationService _userService;
        public UserController(IUserAuthenticationService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(BusinessModelUser model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest();

            return Ok(user);
        }

    }
}
