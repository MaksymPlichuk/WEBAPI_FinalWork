using Microsoft.Extensions.FileProviders;
using WEBAPI_FinalWork.API.Settings;

namespace WEBAPI_FinalWork.API.Extensions
{
    public static class StaticFilesExtension
    {
        public static IApplicationBuilder UseStaticFiles(this IApplicationBuilder app, IWebHostEnvironment web)
        {
            var Items = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(StaticFilesSettings.CarDir,StaticFilesSettings.CarUrl),
                new KeyValuePair<string, string>(StaticFilesSettings.ManufactureDir,StaticFilesSettings.ManufactureUrl)
            };

            string storagePath = Path.Combine(web.ContentRootPath, StaticFilesSettings.Storage);
            if (!Directory.Exists(storagePath)) { Directory.CreateDirectory(storagePath); }

            foreach (var item in Items)
            {
                string filePath = Path.Combine(storagePath, item.Key);
                if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(filePath),
                    RequestPath = item.Value
                });
            }
            return app;
        }
    }
}
