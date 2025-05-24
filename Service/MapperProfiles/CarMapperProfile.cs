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
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Car, CarDtoForCreate>().ReverseMap();
            CreateMap<Car, CarDtoForUpdate>().ReverseMap();
            CreateMap<Car, CarForSaleDto>().ReverseMap();
        }
    }
}
