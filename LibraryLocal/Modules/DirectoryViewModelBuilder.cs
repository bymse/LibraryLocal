using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace LibraryLocal.Modules
{
    public class DirectoryViewModelBuilder
    {
        public static DirectoryViewModel Build(HttpContext context, IEnumerable<IFileInfo> contents)
        {
            var basePath = context.Request.Path.Value;
            var items = contents.Select(e => new ItemViewModel()
            {
                Title = e.Name,
                Url = Uri.EscapeUriString(basePath  + e.Name)
            }).ToArray();

            var pathParts = basePath!.Split("/").Where(e => !string.IsNullOrEmpty(e)).ToArray();
            return new DirectoryViewModel()
            {
                Title = pathParts.Last() + " — Библиотека",
                Items = items,
                BackLink = pathParts.Length == 1 
                    ? null 
                    : "/" + string.Join("/", pathParts.SkipLast(1)),
            };
        }
    }
}