using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.DAL.Entities;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class CarService
    {
        private readonly CarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarService(CarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = _carRepository.GetAll();
            if (entities == null)
            {
                return ServiceResponse.Error("Щось пішло не так");
            }
            return ServiceResponse.Success($"Успішно знайдено {entities.Count()} машин", _mapper.Map<List<CarDto>>(entities));
        }

        public async Task<ServiceResponse> CreateCarAsync(CarDto carDto)
        {
            var entity = _mapper.Map<CarEntity>(carDto);
            var res = await _carRepository.CreateAsync(entity);
            if (!res)
            {
                return ServiceResponse.Error("Щось пішло не так");
            }
            return ServiceResponse.Success($"Авто {carDto.Name} успішно створено!", entity);
        }
    }
}
