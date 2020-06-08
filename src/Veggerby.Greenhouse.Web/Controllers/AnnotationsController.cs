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
    [Authorize(Policy = AuthZ.ReadAll)]
    public class AnnotationsController : ControllerBase
    {
        private readonly GreenhouseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AnnotationsController> _logger;

        public AnnotationsController(GreenhouseContext context, IMapper mapper, ILogger<AnnotationsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{annotationId}")]
        public async Task<IActionResult> Get(int annotationId)
        {
            var annotation = await _context
                .Annotations
                .SingleOrDefaultAsync(x => x.AnnotationId == annotationId);

            if (annotation == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<AnnotationModel>(annotation);

            return Ok(model);
        }

        [HttpPut("{annotationId}")]
        [Authorize(Policy = AuthZ.WriteAll)]
        public async Task<IActionResult> Put(int annotationId, [FromBody] AnnotationModel model)
        {
            var annotation = await _context
                .Annotations
                .FindAsync(annotationId);

            if (annotation == null)
            {
                return NotFound();
            }

            var measurement = await _context
                .Measurements
                .FindAsync(model.MeasurementId);

            if (measurement == null)
            {
                return BadRequest();
            }

            annotation.MeasurementId = model.MeasurementId;
            annotation.Measurement = measurement;
            annotation.Title = model.Title;
            annotation.Body = model.Body;

            _context.Annotations.Update(annotation);
            await _context.SaveChangesAsync();

            model = _mapper.Map<AnnotationModel>(annotation);

            return Ok(model);
        }

        [HttpPost()]
        [Authorize(Policy = AuthZ.WriteAll)]
        public async Task<IActionResult> Post([FromBody] AnnotationModel model)
        {

            var measurement = await _context
                .Measurements
                .FindAsync(model.MeasurementId);

            if (measurement == null)
            {
                return BadRequest();
            }

            var annotation = new Annotation
            {
                MeasurementId = model.MeasurementId,
                Measurement = measurement,
                Title = model.Title,
                Body = model.Body
            };

            await _context.Annotations.AddAsync(annotation);
            await _context.SaveChangesAsync();

            annotation = await _context
                .Annotations
                .SingleOrDefaultAsync(x => x.AnnotationId == annotation.AnnotationId);

            model = _mapper.Map<AnnotationModel>(annotation);

            return Created("/", model);
        }

        [HttpDelete("{annotationId}")]
        [Authorize(Policy = AuthZ.WriteAll)]
        public async Task<IActionResult> HttpDelete(int annotationId)
        {
            var annotation = await _context
                .Annotations
                .SingleOrDefaultAsync(x => x.AnnotationId == annotationId);

            if (annotation == null)
            {
                return BadRequest();
            }

            _context.Annotations.Remove(annotation);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
