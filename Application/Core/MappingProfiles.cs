using Application.Models;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<UpdateActivityDto, Activity>();
        }
    }
}