using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class ImageService
    {
        public async Task<ServiceResponse> SaveImageAsync(IFormFile file, string storagePath)
        {
            try
            {
                var type = file.ContentType.Split("/");
                if (type.Length != 2 || type[0] != "image")
                {
                    return ServiceResponse.Error("Файл не є зображенням");
                }
                string fileExt = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExt;


                string fullSavePath = Path.Combine(storagePath, fileName);

                using var fileStream = File.OpenWrite(fullSavePath);
                await file.CopyToAsync(fileStream);

                return ServiceResponse.Success("Файл успішно записано", fileName);
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }
        }
        public ServiceResponse DeleteImage(string filePath)
        {
            if (!File.Exists(filePath)) { return ServiceResponse.Error("Зображення не існує"); }
            File.Delete(filePath);
            return ServiceResponse.Success($"Файл за шляхом {filePath} успішно видалено");
        }
    }
}
