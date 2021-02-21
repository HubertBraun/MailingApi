using MailingApi.Layers;
using MailingApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly BusinessLayer _buisness;
        public MailingController(BusinessLayer buisness)
        {
            _buisness = buisness;
        }
        /// <summary>
        /// Searchs for a group by Id
        /// </summary>
        /// <returns>Returns one group by Id</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetGroupById(int groupId)
        {
            var group = _buisness.GetBusinessModel(groupId);
            if (group is null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        /// <summary>
        /// Inserts a new group
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostNewGroup(BusinessModelGroup group)
        {
            var id = _buisness.SaveBusinessModelGroup(group);
            if (id != -1)
            {
                return Created("", id); // TODO: routing
            }
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PutGroup(BusinessModelGroup group)
        {
            var actualGroup = _buisness.GetBusinessModel(group.Id);
            
            if (group is null)
            {
                return NotFound();
            }
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteGroup(int groupId)
        {
            var result = _buisness.DeleteBusinessModelGroup(groupId);
            if(result)
            {
                return Ok();
            }
            return NotFound();
        }

    }
}
