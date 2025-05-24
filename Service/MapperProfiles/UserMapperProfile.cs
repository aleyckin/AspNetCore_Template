using AutoMapper;
using Domain.Entities;
using Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserDtoCredentials>().ReverseMap();
            CreateMap<User, UserDtoForUpdate>().ReverseMap();
        }
    }
}
