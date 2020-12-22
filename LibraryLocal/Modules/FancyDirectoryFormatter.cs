using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryLocal.Modules.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace LibraryLocal.Modules
{
    public class FancyDirectoryFormatter : IDirectoryFormatter
    {
        public async Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
        {
            var renderService = context.RequestServices.GetService<IViewRenderService>();
            var viewModel = DirectoryViewModelBuilder.Build(context, contents);
            var data = await renderService!.RenderToStringAsync("Directory/Directory", viewModel);
            var bytes = Encoding.UTF8.GetBytes(data);
            context.Response.ContentLength = bytes.Length;
            await context.Response.Body.WriteAsync(bytes.AsMemory(0, bytes.Length));
        }
    }
}