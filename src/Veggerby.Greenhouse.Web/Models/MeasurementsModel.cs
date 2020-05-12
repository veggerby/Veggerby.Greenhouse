namespace Veggerby.Greenhouse.Web.Models
{
    public class MeasurementsModel
    {
        public DeviceModel Device { get; set; }
        public PropertyModel Property { get; set; }
        public MeasurementModel[] Measurements { get; set; }
    }
}