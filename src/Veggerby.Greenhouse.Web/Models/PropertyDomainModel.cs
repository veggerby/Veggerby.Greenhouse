namespace Veggerby.Greenhouse.Web.Models
{
    public class PropertyDomainModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PropertyDomainValueModel[] Values { get; set; }
    }
}