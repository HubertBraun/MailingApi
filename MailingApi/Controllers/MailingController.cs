using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailingController : ControllerBase
    {

        public MailingController()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="404">Group not found</response> 
        /// <response code="200">OK</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            return NotFound();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="409">Group already exist</response> 
        /// <response code="201">Group created</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post()
        {
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Group not found</response> 
        /// <response code="201">Group created</response> 
        /// <response code="200">Group updated</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Put()
        {
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="404">Group not found</response> 
        /// <response code="200">Group deleted</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete()
        {
            return NotFound();
        }

    }
}
