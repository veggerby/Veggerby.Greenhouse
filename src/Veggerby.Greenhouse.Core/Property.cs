using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class Property
    {
        [Column(Order = 0)]
        public string PropertyId { get; set; }

        [Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public string Unit { get; set; }

        [Column(Order = 3)]
        public double? Tolerance { get; set; } = 0.01;

        [Column(Order = 4)]
        public int Decimals { get; set; } = 3;

        [Column(Order = 5)]
        public string PropertyDomainId { get; set; }

        [Column(Order = 6)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public IList<Measurement> Measurements { get; set; }

        public PropertyDomain PropertyDomain { get; set; }
    }
}
