using GlobalExceptionHandling.Models;
using GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverServices _driverServices;
        private readonly ILogger<DriverController> _logger;

        public DriverController(IDriverServices driverServices, ILogger<DriverController> logger)
        {
            _driverServices = driverServices;
            _logger = logger;
        }

        [HttpGet("DriverList")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _driverServices.GetDrivers();
            return Ok(result);
        }

        [HttpPost("AddDriver")]
        public async Task<IActionResult> Add(Driver driver)
        {
            var result = await _driverServices.AddDriver(driver);
            return Ok(result);
        }

        [HttpGet("DriverId")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _driverServices.GetDriverById(id);
            if (result == null)
            {
                return NotFound();
            }
                
            return Ok(result);
        }

        [HttpPut("UpdateDriver")]
        public async Task<IActionResult> Update(Driver driver)
        {
            var result = await _driverServices.UpdateDriver(driver);
            return Ok(result);
        }

        [HttpDelete("DeleteDriver")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _driverServices.DeleteDriver(id);
            return Ok(result);
        }




    }
}

