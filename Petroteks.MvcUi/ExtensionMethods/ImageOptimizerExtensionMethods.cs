
using System;

using ImageMagick;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Petroteks.MvcUi.ExtensionMethods
{
    public static class ImageOptimizerExtensionMethods
    {
        public static void Optimize(this ImageOptimizer imageOptimizer,string file,[FromServices] ILogger<ImageOptimizer> logger)
        {
            if (imageOptimizer.IsSupported(file))
            {
                try
                {
                    imageOptimizer.Compress(file);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                }
            }
        }
    }
}
