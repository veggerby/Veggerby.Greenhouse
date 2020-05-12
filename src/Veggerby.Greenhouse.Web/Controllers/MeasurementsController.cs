using System;
using System.Collections.Generic;
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
    [Route("[controller]")]
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
            var measurements = await _context
                .Measurements
                .Include(x => x.Device)
                .Include(x => x.Property)
                .Where(x => x.DeviceId == d && x.PropertyId == p)
                .OrderByDescending(x => x.FirstTimeUtc)
                .Take(c)
                .ToListAsync();

            var models = _mapper.Map<MeasurementModel[]>(measurements);

            return Ok(models);
        }
    }
}
