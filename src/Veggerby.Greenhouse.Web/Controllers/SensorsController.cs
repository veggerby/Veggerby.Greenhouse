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
    [Authorize]
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
    }
}
