using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Payload { get; set; } = null;

        public static ServiceResponse Success(string message, object? payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = message,
                Payload = payload
            };
        }
        public static ServiceResponse Error(string message, object? payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Message = message,
                Payload = payload
            };
        }
    }
}
