using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class PropertyDomain
    {
        [Column(Order = 0)]
        public string PropertyDomainId { get; set; }

        [Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public IList<PropertyDomainValue> PropertyDomainValues { get; set; }

        public IList<Property> Properties { get; set; }
    }
}
