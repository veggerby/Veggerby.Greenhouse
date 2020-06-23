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
                .Include(x => x.PropertyDomain)
                    .ThenInclude(x => x.PropertyDomainValues)
                .OrderByDescending(x => x.PropertyId)
                .ToListAsync();

            if (!properties.Any())
            {
                return NoContent();
            }

            var model = _mapper.Map<PropertyModel[]>(properties);

            return Ok(model);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> Get(string propertyId)
        {
            var property = await _context
                .Properties
                .Include(x => x.PropertyDomain)
                    .ThenInclude(x => x.PropertyDomainValues)
                .SingleOrDefaultAsync(x => x.PropertyId == propertyId);

            if (property == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PropertyModel>(property);

            return Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpPut("{propertyId}")]
        public async Task<IActionResult> Put(string propertyId, [FromBody] PropertyModel model)
        {
            var property = await _context
                .Properties
                .SingleOrDefaultAsync(x => x.PropertyId == propertyId);

            var isNew = false;

            if (property == null)
            {
                isNew = true;
                property = new Property { PropertyId = propertyId };
            }

            property.Name = model.Name;
            property.Unit = model.Unit;
            property.Tolerance = model.Tolerance;
            property.Decimals = model.Decimals;

            if (isNew)
            {
                await _context.Properties.AddAsync(property);
            }
            else
            {
                _context.Properties.Update(property);
            }

            await _context.SaveChangesAsync();

            property = await _context
                .Properties
                .SingleOrDefaultAsync(x => x.PropertyId == propertyId);

            model = _mapper.Map<PropertyModel>(property);

            return isNew ? (IActionResult)Created("/", model) : Ok(model);
        }

        [Authorize(Policy = AuthZ.WriteAll)]
        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> Delete(string propertyId)
        {
            var property = await _context
                .Properties
                .SingleOrDefaultAsync(x => x.PropertyId == propertyId);

            if (property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
