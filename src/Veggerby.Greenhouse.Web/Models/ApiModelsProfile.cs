using System.Linq;
using AutoMapper;
using Veggerby.Greenhouse.Core;

namespace Veggerby.Greenhouse.Web.Models
{
    public class ApiModelsProfile : Profile
    {
        public ApiModelsProfile()
        {
            CreateMap<Device, DeviceModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.DeviceId))
                .ReverseMap();

            CreateMap<Sensor, SensorModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.SensorId))
                .ReverseMap();

            CreateMap<Property, PropertyModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.PropertyId))
                .ForPath(x => x.Domain, o => o.MapFrom(x => x.PropertyDomain))
                .ReverseMap();

            CreateMap<PropertyDomain, PropertyDomainModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.PropertyDomainId))
                .ForPath(x => x.Values, o => o.MapFrom(x => x.PropertyDomainValues.OrderBy(v => v.LowerValue)))
                .ReverseMap();

            CreateMap<PropertyDomainValue, PropertyDomainValueModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.PropertyDomainValueId))
                .ReverseMap();

            CreateMap<Measurement, MeasurementModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.MeasurementId))
                .ForPath(x => x.StartTime, o => o.MapFrom(x => x.FirstTimeUtc))
                .ForPath(x => x.EndTime, o => o.MapFrom(x => x.LastTimeUtc))
                .ForPath(x => x.SignalCount, o => o.MapFrom(x => x.Count))
                .ForPath(x => x.Annotations, o => o.MapFrom(x => x.Annotations != null && x.Annotations.Any() ? x.Annotations : null));

            CreateMap<Annotation, AnnotationModel>()
                .ForPath(x => x.Id, o => o.MapFrom(x => x.AnnotationId))
                .ReverseMap();
        }
    }
}