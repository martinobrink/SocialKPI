using AutoMapper;
using SocialKpiApi.Models;

namespace SocialKpiApi.Infrastructure.AutoMapper
{
    public class EventAutoMapperProfile : Profile
    {
        public EventAutoMapperProfile() : base(nameof(EventAutoMapperProfile)) 
        {
            CreateMap<EventInput, Event>();
        }
    }
}
