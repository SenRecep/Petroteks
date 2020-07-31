using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;

namespace Petroteks.MvcUi.Models
{
    public class SearchManager
    {
        private readonly SearchSystem searchSystem;

        public SearchManager(IServiceProvider serviceProvider)
        {
            searchSystem = serviceProvider.GetService<SearchSystem>();
        }
        public void Build(int websiteId, int languageId)
        {
            searchSystem.Init(new List<SearchModel>() {
                new SearchModel(nameof(Product),new List<SearchModelProperty>(){
                    new SearchModelProperty(nameof(Product.Content),true),
                    new SearchModelProperty(nameof(Product.SubTitle))
                }),
                new SearchModel("Category",new List<SearchModelProperty>(){
                    new SearchModelProperty("Name"),
                }),
                new SearchModel("AboutUsObject",new List<SearchModelProperty>(){
                    new SearchModelProperty("Content",true),
                }),
                new SearchModel("Blog",new List<SearchModelProperty>(){
                 new SearchModelProperty("Name"),
                 new SearchModelProperty("Title"),
                 new SearchModelProperty("Content",true),
                })
            }, websiteId, languageId);
        }

        public List<SearchObject> Search(string s)
        {
            searchSystem.SearchObjects.ForEach(entity => entity.Power = 0);
            var serchKeys = s.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            searchSystem.SearchObjects.ForEach(entity =>
            {
                var entitySerchProp = entity.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                serchKeys.ForEach(key =>
                {
                    var found = entitySerchProp.Where(x => x.Contains(key, StringComparison.InvariantCultureIgnoreCase));
                    if (found != null && found?.Count() > 0)
                        entity.Power += found.Count();
                });
            });
            return searchSystem.SearchObjects.Where(x => x.Power > 0).OrderByDescending(x => x.Power).ToList();
        }
    }
}
