using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Veggerby.Greenhouse.Core;
using Veggerby.Greenhouse.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Veggerby.Greenhouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = AuthZ.ReadAll)]
    public class DevicesController : ControllerBase
    {
        private readonly GreenhouseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(GreenhouseContext context, IMapper mapper, ILogger<DevicesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devices = await _context
                .Devices
                .OrderByDescending(x => x.DeviceId)
                .ToListAsync();

            if (!devices.Any())
            {
                return NoContent();
            }

            var model = _mapper.Map<DeviceModel[]>(devices);

            return Ok(model);
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> Get(string deviceId)
        {
            var device = await _context.Devices.SingleOrDefaultAsync(x => x.DeviceId == deviceId);

            if (device == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<DeviceModel>(device);

            return Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpPut("{deviceId}")]
        public async Task<IActionResult> Put(string deviceId, [FromBody] DeviceModel model)
        {
            var device = await _context
                .Devices
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId);

            var isNew = false;

            if (device == null)
            {
                isNew = true;
                device = new Device { DeviceId = deviceId };
            }

            device.Name = model.Name;
            device.Enabled = model.Enabled;

            if (isNew)
            {
                await _context.Devices.AddAsync(device);
            }
            else
            {
                _context.Devices.Update(device);
            }

            await _context.SaveChangesAsync();

            device = await _context
                .Devices
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId);

            model = _mapper.Map<DeviceModel>(device);

            return isNew ? (IActionResult)Created("/", model) : Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> Delete(string deviceId)
        {
            var device = await _context
                .Devices
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId);

            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{deviceId}/sensors")]
        public async Task<IActionResult> GetSensors(string deviceId)
        {
            var device = await _context.Devices.Include(x => x.Sensors).SingleOrDefaultAsync(x => x.DeviceId == deviceId);

            if (device == null)
            {
                return NotFound();
            }

            if (!device.Sensors.Any())
            {
                return NoContent();
            }

            var model = _mapper.Map<SensorModel[]>(device.Sensors);

            return Ok(model);
        }
    }
}
