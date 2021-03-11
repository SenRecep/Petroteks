using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ImageMagick;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Petroteks.MvcUi.Services
{
    public class ExistingImageOptimizer : BackgroundService
    {
        private readonly IWebHostEnvironment environment;
        private readonly ImageOptimizer imageOptimizer;
        private readonly ILogger<ExistingImageOptimizer> logger;

        public ExistingImageOptimizer(IWebHostEnvironment environment, ImageOptimizer imageOptimizer, ILogger<ExistingImageOptimizer> logger)
        {
            this.environment = environment;
            this.imageOptimizer = imageOptimizer;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000);

            logger.LogInformation("Start ExistingImageOptimizer");
            string[] files = Directory.GetFiles(environment.WebRootPath, "*", SearchOption.AllDirectories);
            string[] filters = new string[]
            {
                 ".ico", ".jpeg", ".jpg", ".png", ".gif", ".tiff", ".bmp",
                 ".ICO", ".JPEG", ".JPG", ".PNG", ".GIF", ".TIFF", ".BMP"
            };
            files = files.Where(file => filters.Contains(Path.GetExtension(file))).ToArray();
            int index = 0;
            int errorCount = 0;

            while (!stoppingToken.IsCancellationRequested && index < files.Length)
            {
                string file = files[index++];
                if (imageOptimizer.IsSupported(file))
                {
                    try
                    {
                        imageOptimizer.Compress(file);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message, "ExistingImageOptimizer");
                        errorCount++;
                    }
                }
            }

            logger.LogInformation($"ExistingImageOptimizer Work done Error Count : {errorCount}");
        }
    }
}
