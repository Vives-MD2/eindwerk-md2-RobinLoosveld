using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunderstruck.BLL.Managers;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLocationDataController : ControllerBase
    {
        private readonly UserLocationDataManager _ulDataManager = new UserLocationDataManager();

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery(Name = "userId")] int userId,
            [FromQuery(Name = "locationDataId")] int locationDataId)
        {
            try
            {
                if (userId <= 0 || locationDataId <= 0)
                {
                    var badEntity = new ThunderstruckException
                    {
                        Source = "One or both IDs are invald, try something else.",
                        EType = ExceptionTypes.Warning
                    };
                    return BadRequest(badEntity.Source);
                }
                var userLocationData = _ulDataManager.GetByIdAsync(userId, locationDataId);
                return Ok(new JsonResult(userLocationData));
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
                IEnumerable<UserLocationData> userLocationsData = await _ulDataManager.GetAsync(skip, take);
                return Ok(userLocationsData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        //[ValidateModel]
        public async Task<IActionResult> Update([FromBody] UserLocationData userLocationData)
        {
            try
            {
                if (userLocationData == null)
                {
                    var badEntity = new ThunderstruckException
                    {
                        Source = "One or both IDs are invald, try something else.",
                        EType = ExceptionTypes.Warning
                    };
                    return BadRequest(badEntity.Source);
                }
                var dbUserLocationData = await _ulDataManager.UpdateAsync(userLocationData);
                return Ok(new JsonResult(dbUserLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Delete")]
        //[ValidateModel]
        public async Task<IActionResult> Delete([FromBody] UserLocationData userLocationData)
        {
            try
            {
                if (userLocationData == null)
                {
                    var badEntity = new ThunderstruckException
                    {
                        Source = "One or both IDs are invald, try something else.",
                        EType = ExceptionTypes.Warning
                    };
                    return BadRequest(badEntity.Source);
                }
                var dbUserLocationData = await _ulDataManager.DeleteAsync(userLocationData);
                return Ok(new JsonResult(dbUserLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
