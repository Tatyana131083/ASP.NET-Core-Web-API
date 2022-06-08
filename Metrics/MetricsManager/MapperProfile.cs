using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Models;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentCreateRequest, AgentInfo>().
                 ForMember(x => x.Url, opt => opt.MapFrom(src => src.Url));
        }

    }
}

