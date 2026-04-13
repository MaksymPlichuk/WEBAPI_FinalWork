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
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = _carRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return ServiceResponse.Error($"Авто з Id {id} не існує");
            }
            return ServiceResponse.Success($"Авто з Id {id} успішно знайдено", _mapper.Map<CarDto>(entity));
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var entity = _carRepository.GetByNameAsync(name);
            if (entity == null)
            {
                return ServiceResponse.Error($"Авто з назвою {name} не існує");
            }
            return ServiceResponse.Success($"Авто з назвою {name} успішно знайдено", _mapper.Map<CarDto>(entity));
        }


        public async Task<ServiceResponse> CreateCarAsync(CreateCarDto dto)
        {
            var entity = _mapper.Map<CarEntity>(dto);
            var res = await _carRepository.CreateAsync(entity);
            if (!res)
            {
                return ServiceResponse.Error("Щось пішло не так");
            }
            return ServiceResponse.Success($"Авто {dto.Name} успішно створено!", entity);
        }

        public async Task<ServiceResponse> UpdateCarAsync(UpdateCarDto dto)
        {
            var entity = await _carRepository.GetByIdAsync(dto.Id);
            if (entity == null) return ServiceResponse.Error("Щось пішло не так");

            var updatedEntity = _mapper.Map(dto,entity);


            var res = await _carRepository.UpdateAsync(updatedEntity);
            if (!res)
            {
                return ServiceResponse.Error("Щось пішло не так");
            }
            return ServiceResponse.Success($"Авто {dto.Name} успішно оновлено!", updatedEntity);
        }


        public async Task<ServiceResponse> DeleteByIdAsync(int id)
        {
            var entity = await _carRepository.GetByIdAsync(id);
            if (entity == null) return ServiceResponse.Error("Щось пішло не так");

            bool res = await _carRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error("Щось пішло не так");

            return ServiceResponse.Success($"Авто {entity.Name} {entity.Year} року успішно видалено!", _mapper.Map<CarDto>(entity));
        }
        public async Task<ServiceResponse> DeleteByName(string name)
        {
            var entity = await _carRepository.GetByNameAsync(name);
            if (entity == null) return ServiceResponse.Error("Щось пішло не так");

            bool res = await _carRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error("Щось пішло не так");

            return ServiceResponse.Success($"Авто {entity.Name} {entity.Year} року успішно видалено!", _mapper.Map<CarDto>(entity));
        }
    }
}
