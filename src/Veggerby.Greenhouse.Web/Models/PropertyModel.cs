namespace Veggerby.Greenhouse.Web.Models
{
    public class PropertyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Tolerance { get; set; }
        public int Decimals { get; set; }
        public PropertyDomainModel Domain { get; set; }
    }
}