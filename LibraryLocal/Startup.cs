using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryLocal.Modules;
using LibraryLocal.Modules.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace LibraryLocal
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "/files"));

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/files"),
                FileProvider = new PhysicalFileProvider(configuration["FilesRootAbsolute"]),
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                StaticFileOptions = { ServeUnknownFileTypes = true},
                RedirectToAppendTrailingSlash = true,
                DirectoryBrowserOptions =
                {
                    Formatter = new FancyDirectoryFormatter()
                }
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/front",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "front")),
            });
        }
    }
}