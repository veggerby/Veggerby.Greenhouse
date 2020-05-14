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
                .ReverseMap();

            CreateMap<Measurement, MeasurementModel>()
                .ForPath(x => x.StartTime, o => o.MapFrom(x => x.FirstTimeUtc))
                .ForPath(x => x.EndTime, o => o.MapFrom(x => x.LastTimeUtc))
                .ForPath(x => x.SignalCount, o => o.MapFrom(x => x.Count));
        }
    }
}