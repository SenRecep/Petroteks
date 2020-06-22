using Microsoft.AspNetCore.Hosting;
using Petroteks.MvcUi.ExtensionMethods;
using System;
using System.IO;

namespace Petroteks.MvcUi.Models
{
    public class SavedWebsiteFactory : ISavedWebsiteFactory
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string tempFolder;
        private readonly string websiteTempFile;
        public SavedWebsiteFactory(IServiceProvider serviceProvider)
        {
            webHostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
            tempFolder = Path.Combine(webHostEnvironment.WebRootPath, "Temp");
            websiteTempFile = Path.Combine(tempFolder, "WebsiteTemp.json");
        }

        //public bool IsLoaded => WebsiteContext.SavedWebsite != null;

        //public void Load()
        //{
        //    SavedWebsite temp;
        //    if (!IsLoaded)
        //    {
        //        if (File.Exists(websiteTempFile))
        //            temp = JsonConvert.DeserializeObject<SavedWebsite>(File.ReadAllText(websiteTempFile));
        //        else
        //        {
        //            temp = new SavedWebsite();
        //            string json = JsonConvert.SerializeObject(temp, Formatting.Indented);
        //            File.WriteAllText(websiteTempFile, json);
        //        }
        //        WebsiteContext.SavedWebsite = temp;
        //    }
        //}
        //public void Save()
        //{
        //    if (File.Exists(websiteTempFile))
        //    {
        //        string json = JsonConvert.SerializeObject(WebsiteContext.SavedWebsite, Formatting.Indented);
        //        File.WriteAllText(websiteTempFile, json);
        //    }
        //}
    }
}
