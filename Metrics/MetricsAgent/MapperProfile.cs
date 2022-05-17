using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Requests;
using MetricsAgent.DAL.Response;
using System;

namespace MetricsAgent
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

            CreateMap<HddMetric, HddMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

            CreateMap<RamMetric, RamMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

            CreateMap<NetworkMetric, NetworkMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

            CreateMap<DotNetMetric, DotNetMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

            CreateMap<CpuMetricCreateRequest, CpuMetric>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.TotalSeconds)).
                ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<HddMetricCreateRequest, HddMetric>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.TotalSeconds)).
                ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<RamMetricCreateRequest, RamMetric>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.TotalSeconds)).
                ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<NetworkMetricCreateRequest, NetworkMetric>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.TotalSeconds)).
                ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<DotNetMetricCreateRequest, DotNetMetric>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.TotalSeconds)).
                ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value));

        }
        
    }
}
