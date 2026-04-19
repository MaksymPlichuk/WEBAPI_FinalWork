using WEBAPI_FinalWork.BLL.Services;

namespace WEBAPI_FinalWork.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var resp = ServiceResponse.Error(ex.Message);
                await context.Response.WriteAsJsonAsync(resp);
            }
            
        }
    }
}
