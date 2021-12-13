using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Thunderstruck.BLL.Managers;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager = new UserManager(); 
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id)
        {
            try
            {
                var user = await _userManager.GetByIdAsync(id);
                return Ok(new JsonResult(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take)
        {
            try
            {
                if (take == 0)
                {
                    take = 1;
                }

                IEnumerable<User> users = await _userManager.GetAsync(skip, take);
                return Ok(new JsonResult(users));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    //return user = new User()
                    //{
                    //    Tex = new ThunderstruckException("No user object found.", ExceptionTypes.Fatal)
                    //};
                    throw new NullReferenceException();
                }
                user = await _userManager.CreateAsync(user);
                return Ok(new JsonResult(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    //return user = new User()
                    //{
                    //    Tex = new ThunderstruckException("No user object found.", ExceptionTypes.Fatal)
                    //};
                    throw new NullReferenceException();
                }
                user = await _userManager.UpdateAsync(user);
                return Ok(new JsonResult(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    throw new NullReferenceException();
                }

                user = await _userManager.DeleteAsync(user);
                return Ok(new JsonResult(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
