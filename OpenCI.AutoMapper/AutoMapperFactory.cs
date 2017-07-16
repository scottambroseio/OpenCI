using AutoMapper;
using OpenCI.Business.Models;
using OpenCI.Data.Entities;

namespace OpenCI.AutoMapper
{
    public static class AutoMapperFactory
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectModel>();
                cfg.CreateMap<Plan, PlanModel>();
            });

            return config.CreateMapper();
        }
    }
}