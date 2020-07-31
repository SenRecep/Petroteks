using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Petroteks.Core.Entities;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Models
{
    public class SearchSystem
    {
        private readonly IServiceProvider serviceProvider;
        private readonly DbServiceProvider dbServiceProvider;

        public List<SearchObject> SearchObjects { get; set; }

        public SearchSystem(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            dbServiceProvider = serviceProvider.GetService<DbServiceProvider>();
            SearchObjects = new List<SearchObject>();
        }
        public void Init(List<SearchModel> searchModels, int websiteId, int languageId)
        {
            string typesdll = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Petroteks.Entities.dll");
            List<Type> types = Assembly.LoadFrom(typesdll).GetTypes()
             .Where(t => typeof(EntityBase).IsAssignableFrom(t)).ToList();

            searchModels.ForEach(item =>
            {
                var type = types.FirstOrDefault(t => t.Name.Equals(item.ModelName));
                var lst = item.GetSearchObjects(type, dbServiceProvider,websiteId,languageId);
                SearchObjects.AddRange(lst);
            });
        }
    }
}
