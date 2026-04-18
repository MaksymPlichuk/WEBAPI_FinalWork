using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.BLL.Dtos.Manufacture;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.MapperProfiles
{
    public class ManufactureMapperProfile : Profile
    {
        public ManufactureMapperProfile()
        {
            CreateMap<ManufacturerEntity,ManufactureDto>();

            CreateMap<ManufactureDto, ManufacturerEntity>();

            CreateMap<CreateManufactureDto, ManufacturerEntity>();

            CreateMap<UpdateManufactureDto, ManufacturerEntity>();

            CreateMap<ManufacturerEntity, ManufactureForCarsDto>();
        }
    }
}
