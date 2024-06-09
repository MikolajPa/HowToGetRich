using AutoMapper;
using EnovationApp.DataAccess.Models;
using EnovationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Animal, AnimalDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.AddYears(-src.DayOfBirth.Year).Year))
                .ReverseMap()
                .ForMember(dest => dest.DayOfBirth, opt => opt.MapFrom(src => DateTime.Now.AddYears(-src.Age)))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<UserRequest, User>();

        }
    }
}
