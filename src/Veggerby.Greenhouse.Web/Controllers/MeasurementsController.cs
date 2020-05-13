using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Veggerby.Greenhouse.Core;
using Veggerby.Greenhouse.Web.Models;
using System;

namespace Veggerby.Greenhouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Get([FromQuery(Name="d")] string[] d, string p, int h = 24)
        {
            if (d == null || !d.Any() || string.IsNullOrEmpty(p))
            {
                return BadRequest();
            }

            var devices = await _context.Devices.Where(x => d.Contains(x.DeviceId)).ToListAsync();

            if (devices == null)
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
                .Where(x => d.Contains(x.DeviceId) && x.PropertyId == p && x.FirstTimeUtc > time)
                .OrderByDescending(x => x.FirstTimeUtc)
                .ToListAsync();

            measurements.Reverse();

            if (!measurements.Any())
            {
                return NoContent();
            }

            var model = measurements
                .GroupBy(x => x.Device)
                .Select(m => new MeasurementsModel
                    {
                        Device = _mapper.Map<DeviceModel>(m.Key),
                        Property = _mapper.Map<PropertyModel>(property),
                        Measurements = _mapper.Map<MeasurementModel[]>(m.ToList())
                    }).ToArray();

            return Ok(model);
        }
    }
}
