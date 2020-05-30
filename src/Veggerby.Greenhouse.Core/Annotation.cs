using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class Annotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int AnnotationId { get; set; }

        [Column(Order = 1)]
        public int MeasurementId { get; set; }

        [Column(Order = 2)]
        public string Title { get; set; }

        [Column(Order = 3)]
        public string Body { get; set; }

        [Column(Order = 3)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public Measurement Measurement { get; set; }
    }
}