namespace Veggerby.Greenhouse.Web.Models
{
    public class SensorModel
    {
        public string Id { get; set; }
        public string Key => $"{Id}@{(Device?.Id ?? "(unknown)")}";
        public DeviceModel Device { get; set; }
        public string Name { get; set; }
    }
}