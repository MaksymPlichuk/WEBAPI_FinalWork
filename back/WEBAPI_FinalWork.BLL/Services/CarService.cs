using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.BLL.Dtos.Pagination;
using WEBAPI_FinalWork.DAL.Entities;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class CarService : PaginationService
    {
        private readonly CarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        public CarService(CarRepository carRepository, IMapper mapper, ImageService imageService) : base(mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<ServiceResponse> GetAllAsync(PaginationDto pagination)
        {
            var entities = _carRepository.GetAll().Include(c=>c.Manufacturer);
            if (entities == null)
            {
                return ServiceResponse.Error("Невдалося знайти авто");
            }
            var resp = await GetPaginationAsync<CarEntity, CarDto>(entities, pagination);
            return ServiceResponse.Success($"Успішно знайдено {entities.Count()} машин", resp);
        }
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _carRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return ServiceResponse.Error($"Авто з Id '{id}' не існує");
            }

            var manuf = _carRepository.GetAll().Where(c => c.Id == entity.Id).Include(c => c.Manufacturer).ToList();
            entity.Manufacturer = manuf.First().Manufacturer;

            return ServiceResponse.Success($"Авто з Id '{id}' успішно знайдено", _mapper.Map<CarDto>(entity));
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var entities = await _carRepository.GetByNameAsync(name);
            if (entities == null)
            {
                return ServiceResponse.Error($"Авто з назвою '{name}' не існує");
            }
            return ServiceResponse.Success($"Авто з назвою '{name}' успішно знайдено", _mapper.Map<List<CarDto>>(entities));
        }


        public async Task<ServiceResponse> CreateCarAsync(CreateCarDto dto, string storagePath)
        {
            var entity = _mapper.Map<CarEntity>(dto);
            if (dto.Image != null)
            {
                var resp = await _imageService.SaveImageAsync(dto.Image, storagePath);
                if (!resp.IsSuccess) { return resp; }
                entity.Image = resp.Payload.ToString();
            }


            bool res = await _carRepository.CreateAsync(entity);
            if (!res)
            {
                return ServiceResponse.Error("Невдалося Створити");
            }
            return ServiceResponse.Success($"Авто '{dto.Name}' успішно створено!", _mapper.Map<CarDto>(entity));
        }

        public async Task<ServiceResponse> UpdateCarAsync(UpdateCarDto dto, string storagePath)
        {
            var entity = await _carRepository.GetByIdAsync(dto.Id);
            if (entity == null) return ServiceResponse.Error($"Авто з Id '{dto.Id}' не існує");

            var updatedEntity = _mapper.Map(dto, entity);

            if (dto.Image != null)
            {
                if (entity.Image != null)
                {
                    if (!string.IsNullOrEmpty(updatedEntity.Image))
                    {
                        var resp = _imageService.DeleteImage(Path.Combine(storagePath, entity.Image));
                        if (!resp.IsSuccess) { return resp; }
                    }
                }
                var imgResp = await _imageService.SaveImageAsync(dto.Image, storagePath);
                if (!imgResp.IsSuccess) { return imgResp; }
                updatedEntity.Image = imgResp.Payload.ToString();
            }

            var res = await _carRepository.UpdateAsync(updatedEntity);
            if (!res)
            {
                return ServiceResponse.Error("Невдалося Оновити");
            }
            return ServiceResponse.Success($"Авто '{dto.Name}' успішно оновлено!", _mapper.Map<CarDto>(updatedEntity));
        }


        public async Task<ServiceResponse> DeleteByIdAsync(int id, string storagePath)
        {
            var entity = await _carRepository.GetByIdAsync(id);
            if (entity == null) return ServiceResponse.Error($"Авто з Id '{id}' не існує");


            if (entity.Image!=null && !string.IsNullOrEmpty(entity.Image))
            {
                var resp = _imageService.DeleteImage(Path.Combine(storagePath, entity.Image));
                if (!resp.IsSuccess) { return resp; }
            }


            bool res = await _carRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error("Невдалося Видалити");

            return ServiceResponse.Success($"Авто {entity.Name} {entity.Year} року успішно видалено!", _mapper.Map<CarDto>(entity));
        }
        public async Task<ServiceResponse> DeleteByName(string name, string storagePath)
        {
            var entity = await _carRepository.GetByNameAsync(name);
            if (entity == null) return ServiceResponse.Error($"Авто з назвою '{name}' не існує");

            if (entity.Count > 1) { return ServiceResponse.Error($"Існує {entity.Count} авто з назвою '{name}'! не можна видалити"); }
            var car = entity.First();

            if (car.Image != null && !string.IsNullOrEmpty(car.Image))
            {
                var resp = _imageService.DeleteImage(Path.Combine(storagePath, car.Image));
                if (!resp.IsSuccess) { return resp; }
            }

            bool res = await _carRepository.DeleteAsync(car);
            if (!res) return ServiceResponse.Error("Невдалося Видалити");

            return ServiceResponse.Success($"Авто '{car.Name}' {car.Year} року успішно видалено!", _mapper.Map<CarDto>(car));
        }
    }
}
