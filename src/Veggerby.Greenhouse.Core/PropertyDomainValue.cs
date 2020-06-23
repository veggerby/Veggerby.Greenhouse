using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class PropertyDomainValue
    {
        [Column(Order = 0)]
        public string PropertyDomainValueId { get; set; }

        [Column(Order = 1)]
        public string PropertyDomainId { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public double LowerValue { get; set; }

        [Column(Order = 4)]
        public double UpperValue { get; set; }

        [Column(Order = 5)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public PropertyDomain PropertyDomain { get; set; }
   }
}