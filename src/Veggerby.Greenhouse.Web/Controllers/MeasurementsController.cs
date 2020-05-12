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
        public async Task<IActionResult> Get(string d, string p, int c = 100)
        {
            if (string.IsNullOrEmpty(d) || string.IsNullOrEmpty(p))
            {
                return BadRequest();
            }

            var device = await _context.Devices.FindAsync(d);

            if (device == null)
            {
                return BadRequest();
            }

            var property = await _context.Properties.FindAsync(p);

            if (property == null)
            {
                return BadRequest();
            }

            var measurements = await _context
                .Measurements
                .Where(x => x.DeviceId == d && x.PropertyId == p)
                .OrderByDescending(x => x.FirstTimeUtc)
                .Take(c)
                .ToListAsync();

            measurements.Reverse();

            if (!measurements.Any())
            {
                return NoContent();
            }

            var model = new MeasurementsModel
            {
                Device = _mapper.Map<DeviceModel>(device),
                Property = _mapper.Map<PropertyModel>(property),
                Measurements = _mapper.Map<MeasurementModel[]>(measurements)
            };

            return Ok(model);
        }
    }
}
