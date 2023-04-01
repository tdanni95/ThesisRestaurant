using Microsoft.Extensions.FileProviders;

namespace ThesisRestaurant.Api
{
    public static class FileSystem
    {
        public static WebApplication AllowAccessToUploadedFiles(this WebApplication app)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/uploads"
            });

            return app;
        }
    }
}
