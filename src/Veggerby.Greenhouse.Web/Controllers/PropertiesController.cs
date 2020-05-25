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
    public class PropertiesController : ControllerBase
    {
        private readonly GreenhouseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(GreenhouseContext context, IMapper mapper, ILogger<PropertiesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _context
                .Properties
                .OrderByDescending(x => x.PropertyId)
                .ToListAsync();

            if (!properties.Any())
            {
                return NoContent();
            }

            var model = _mapper.Map<PropertyModel[]>(properties);

            return Ok(model);
        }
    }
}
