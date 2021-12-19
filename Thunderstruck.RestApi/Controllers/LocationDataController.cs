using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunderstruck.BLL.Managers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationDataController : ControllerBase
    {
        private readonly LocationDataManager _ldManager = new LocationDataManager();
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id)
        {
            try
            {
                var dbLocationData = await _ldManager.GetByIdAsync(id);
                return Ok(new JsonResult(dbLocationData));
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

                IEnumerable<LocationData> dbLocationsData = await _ldManager.GetAsync(skip, take);
                return Ok(new JsonResult(dbLocationsData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LocationData locationData)
        {
            try
            {
                if (locationData is null)
                {
                    //return user = new User()
                    //{
                    //    Tex = new ThunderstruckException("No user object found.", ExceptionTypes.Fatal)
                    //};
                    throw new NullReferenceException();
                }
                var dbLocationData = await _ldManager.CreateAsync(locationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //TODO: check if update is needed in project
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] LocationData locationData)
        {
            try
            {
                if (locationData is null)
                {
                    throw new NullReferenceException();
                }

                var dbLocationData = await _ldManager.UpdateAsync(locationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //TODO: check if delete is needed in project
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] LocationData locationData)
        {
            try
            {
                if (locationData is null)
                {
                    throw new NullReferenceException();
                }
                var dbLocationData = await _ldManager.DeleteAsync(locationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
