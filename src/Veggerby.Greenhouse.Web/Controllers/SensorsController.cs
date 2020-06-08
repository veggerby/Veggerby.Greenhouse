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
    public class SensorsController : ControllerBase
    {
        private readonly GreenhouseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SensorsController> _logger;

        public SensorsController(GreenhouseContext context, IMapper mapper, ILogger<SensorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sensors = await _context
                .Sensors
                .Include(x => x.Device)
                .OrderByDescending(x => x.DeviceId)
                .ToListAsync();

            if (!sensors.Any())
            {
                return NoContent();
            }

            var model = _mapper.Map<SensorModel[]>(sensors);

            return Ok(model);
        }

        [HttpGet("{deviceId}/{sensorId}")]
        public async Task<IActionResult> Get(string deviceId, string sensorId)
        {
            var sensor = await _context
                .Sensors
                .Include(x => x.Device)
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId && x.SensorId == sensorId);

            if (sensor == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<SensorModel>(sensor);

            return Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpPut("{deviceId}/{sensorId}")]
        public async Task<IActionResult> Put(string deviceId, string sensorId, [FromBody] SensorModel model)
        {
            var sensor = await _context
                .Sensors
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId && x.SensorId == sensorId);

            var isNew = false;

            if (sensor == null)
            {
                var device = await _context
                    .Devices
                    .SingleOrDefaultAsync(x => x.DeviceId == deviceId);

                if (device == null)
                {
                    return NotFound();
                }

                isNew = true;
                sensor = new Sensor { SensorId = sensorId, DeviceId = deviceId };
            }

            sensor.Name = model.Name;
            sensor.Enabled = model.Enabled;

            if (isNew)
            {
                await _context.Sensors.AddAsync(sensor);
            }
            else
            {
                _context.Sensors.Update(sensor);
            }

            await _context.SaveChangesAsync();

            sensor = await _context
                .Sensors
                .Include(x => x.Device)
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId && x.SensorId == sensorId);

            model = _mapper.Map<SensorModel>(sensor);

            return isNew ? (IActionResult)Created("/", model) : Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpDelete("{deviceId}/{sensorId}")]
        public async Task<IActionResult> Delete(string deviceId, string sensorId)
        {
            var sensor = await _context
                .Sensors
                .SingleOrDefaultAsync(x => x.DeviceId == deviceId && x.SensorId == sensorId);

            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
