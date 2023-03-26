using Microsoft.Extensions.FileProviders;

namespace ThesisRestaurant.Api
{
    public static class FileSystem
    {
        public static WebApplication AllowAccessToUploadedFiles(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
                RequestPath = "/uploads"
            });

            return app;
        }
    }
}
