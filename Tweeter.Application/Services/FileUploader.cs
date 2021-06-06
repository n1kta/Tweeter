using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Tweeter.Domain.Contracts;

namespace Tweeter.Application.Services
{
    public class FileUploader : IFileUploader
    {
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _context;

        private const string HTTP = "http";
        private const string PNG_EXTENSION = ".png";
        private const string IMAGE_DIR = "Resources/Images";
        
        public FileUploader(IHostingEnvironment env,
                            IHttpContextAccessor context)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _context = context;
        }
        
        public string Upload(string file)
        {
            var rootDir = $"{_env.ContentRootPath}/{IMAGE_DIR}";
            var bytes = Convert.FromBase64String(file);

            var fileName = Guid.NewGuid();
            
            var path = $"{rootDir}/{fileName}{PNG_EXTENSION}";
            var photoPath = $"{HTTP}://{_context.HttpContext.Request.Host.Value}/{IMAGE_DIR}/{fileName}{PNG_EXTENSION}";
            
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return photoPath;
        }
    }
}