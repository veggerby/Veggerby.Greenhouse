using System;

namespace Veggerby.Greenhouse.Web.Models
{
    public class AnnotationModel
    {
        public int Id { get; set; }
        public int MeasurementId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedUtc { get; set; }

    }
}