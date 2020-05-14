namespace Veggerby.Greenhouse.Web.Models
{
    public class MeasurementsModel
    {
        public SensorModel Sensor { get; set; }
        public PropertyModel Property { get; set; }
        public MeasurementModel[] Measurements { get; set; }
    }
}