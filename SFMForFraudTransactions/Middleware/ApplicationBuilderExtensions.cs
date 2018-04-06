using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Builder for middleware
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Extension Class to copy node_modules folder to wwwroot in development
        /// </summary>
        /// <param name="app"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = fileProvider;

            app.UseStaticFiles(options);

            return app;
        }
    }
}
