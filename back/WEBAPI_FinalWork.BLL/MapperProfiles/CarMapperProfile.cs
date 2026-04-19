using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.BLL.Services;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.MapperProfiles
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile()
        {
            CreateMap<CarEntity, CarDto>();

            CreateMap<CreateCarDto, CarEntity>()
                .ForMember(dest=>dest.Image, opt=>opt.Ignore());

            CreateMap<UpdateCarDto, CarEntity>()
                .ForMember(dest=>dest.Image, opt=>opt.Ignore());

        }
    }
}
