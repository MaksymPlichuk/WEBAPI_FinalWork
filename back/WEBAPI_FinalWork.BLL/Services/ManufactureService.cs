using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.BLL.Dtos.Manufacture;
using WEBAPI_FinalWork.BLL.Dtos.Pagination;
using WEBAPI_FinalWork.DAL.Entities;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class ManufactureService : PaginationService
    {
        private readonly ManufactureRepository _manufactureRepository;
        private readonly IMapper _mapper;
        public ManufactureService(ManufactureRepository manufactureRepository, IMapper mapper) : base(mapper)
        {
            _manufactureRepository = manufactureRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync(PaginationDto pagination)
        {
            var entities = _manufactureRepository.GetAll().Include(m=>m.Cars);
            if (entities == null)
            {
                return ServiceResponse.Error("Невдалося знайти виробників");
            }
            var paginatedResp = await GetPaginationAsync<ManufacturerEntity, ManufactureDto>(entities, pagination);
            return ServiceResponse.Success($"Успішно знайдено {entities.Count()} виробників", paginatedResp);
        }
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = _manufactureRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return ServiceResponse.Error($"Виробник з Id {id} не існує");
            }
            return ServiceResponse.Success($"Виробник з Id {id} успішно знайдено", _mapper.Map<ManufactureDto>(entity));
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var entity = _manufactureRepository.GetByNameAsync(name);
            if (entity == null)
            {
                return ServiceResponse.Error($"Виробник з назвою {name} не існує");
            }
            return ServiceResponse.Success($"Виробник з назвою {name} успішно знайдено", _mapper.Map<ManufactureDto>(entity));
        }


        public async Task<ServiceResponse> CreateManufacturerAsync(CreateManufactureDto dto)
        {
            var entity = _mapper.Map<ManufacturerEntity>(dto);
            var res = await _manufactureRepository.CreateAsync(entity);
            if (!res)
            {
                return ServiceResponse.Error("Невдалося Створити");
            }
            return ServiceResponse.Success($"Виробник {dto.Name} успішно створено!", _mapper.Map<ManufactureDto>(entity));
        }

        public async Task<ServiceResponse> UpdateManufacturerAsync(UpdateManufactureDto dto)
        {
            var entity = await _manufactureRepository.GetByIdAsync(dto.Id);
            if (entity == null) return ServiceResponse.Error($"Виробника з Id {dto.Id} не існує!");

            var updatedEntity = _mapper.Map(dto, entity);


            var res = await _manufactureRepository.UpdateAsync(updatedEntity);
            if (!res)
            {
                return ServiceResponse.Error("Невдалося Оновити");
            }
            return ServiceResponse.Success($"Виробник {dto.Name} успішно оновлено!", _mapper.Map<ManufactureDto>(entity));
        }


        public async Task<ServiceResponse> DeleteByIdAsync(int id)
        {
            var entity = await _manufactureRepository.GetByIdAsync(id);
            if (entity == null) return ServiceResponse.Error($"Виробника з Id {id} не існує!");

            bool res = await _manufactureRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error("Невдалося Видалити");

            return ServiceResponse.Success($"Виробник {entity.Name} успішно видалено!", _mapper.Map<ManufactureDto>(entity));
        }
        public async Task<ServiceResponse> DeleteByName(string name)
        {
            var entity = await _manufactureRepository.GetByNameAsync(name);
            if (entity == null) return ServiceResponse.Error($"Виробника {name} не знайдено!");

            bool res = await _manufactureRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error("Невдалося Видалити");

            return ServiceResponse.Success($"Виробник {entity.Name} успішно видалено!", _mapper.Map<ManufactureDto>(entity));
        }
    }
}
