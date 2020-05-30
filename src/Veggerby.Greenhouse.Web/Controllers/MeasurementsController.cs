using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Veggerby.Greenhouse.Core;
using Veggerby.Greenhouse.Web.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Veggerby.Greenhouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeasurementsController : ControllerBase
    {
        private readonly GreenhouseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MeasurementsController> _logger;

        public MeasurementsController(GreenhouseContext context, IMapper mapper, ILogger<MeasurementsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name="d")] string[] d, [FromQuery(Name="s")] string[] s, string p, int h = 24, int c = 0)
        {
            d = d ?? Array.Empty<string>();
            s = s ?? Array.Empty<string>();

            if (!(d.Any() || s.Any()) || (d.Any() && s.Any()) || string.IsNullOrEmpty(p))
            {
                return BadRequest();
            }

            var devices = await _context.Devices.Where(x => d.Contains(x.DeviceId)).ToListAsync();

            var sensors = await _context.Sensors.Where(x => s.Contains(x.SensorId + "@" + x.DeviceId)).ToListAsync();

            if ((devices?.Any() ?? false) && (sensors?.Any() ?? false))
            {
                return BadRequest();
            }

            var property = await _context.Properties.FindAsync(p);

            if (property == null)
            {
                return BadRequest();
            }

            var time = DateTime.UtcNow.AddHours(-h);

            var measurements = await _context
                .Measurements
                .Include(x => x.Device)
                .Include(x => x.Sensor)
                    .ThenInclude(x => x.Device)
                .Include(x => x.Annotations)
                .Where(x => (d.Contains(x.DeviceId) || s.Contains(x.SensorId + "@" + x.DeviceId)) && x.PropertyId == p && x.FirstTimeUtc > time)
                .OrderByDescending(x => x.FirstTimeUtc)
                .Take(c > 0 ? c : int.MaxValue)
                .ToListAsync();

            if (!measurements.Any())
            {
                return NoContent();
            }

            var model = measurements
                .GroupBy(x => x.Sensor)
                .OrderBy(x => x.Key.SensorId).ThenBy(x => x.Key.DeviceId)
                .Select(m => new MeasurementsModel
                    {
                        Sensor = _mapper.Map<SensorModel>(m.Key),
                        Property = _mapper.Map<PropertyModel>(property),
                        Measurements = _mapper.Map<MeasurementModel[]>(m.OrderBy(x => x.FirstTimeUtc).ToList())
                    }).ToArray();

            return Ok(model);
        }
    }
}
