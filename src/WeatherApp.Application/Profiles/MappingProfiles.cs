using System.Runtime.CompilerServices;
using AutoMapper;
using WeatherApp.Application.DTOs;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Profiles
{
    public class MappingProfiles : Profile {
        public MappingProfiles(){
            CreateMap<Weather, WeatherDto>().ReverseMap();
        }
    }
}