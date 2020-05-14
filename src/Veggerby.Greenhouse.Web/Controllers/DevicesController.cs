using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Veggerby.Greenhouse.Core;
using Veggerby.Greenhouse.Web.Models;

namespace Veggerby.Greenhouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
