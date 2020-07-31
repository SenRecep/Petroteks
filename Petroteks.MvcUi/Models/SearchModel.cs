using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Petroteks.Core.Dal;
using Petroteks.Core.Entities;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Models
{
    public class SearchModel
    {
        private readonly ICollection<SearchModelProperty> searchModelProperties;
        public SearchModel(string model, ICollection<SearchModelProperty> searchModelProperties)
        {
            ModelName = model;
            this.searchModelProperties = searchModelProperties;
        }
        public string ModelName { get; }

        public List<SearchObject> GetSearchObjects(Type type, DbServiceProvider dbService, int websiteId, int languageId)
        {
            var searchObjects = new List<SearchObject>();
            var repo = DbServiceProvider.Execute(type, dbService);
            var datas = repo.LanguageAndWebsiteFilteredData(websiteId, languageId);
            foreach (var obj in datas)
            {
                foreach (SearchModelProperty item in searchModelProperties)
                {
                    var prop = type.GetProperty(item.PropertyName);
                    var propvalue = prop?.GetValue(obj)?.ToString();
                    if (propvalue != null)
                    {
                        if (item.IsHtml)
                        {
                            propvalue = propvalue.Replace("&nbsp;", string.Empty);
                            propvalue = Regex.Replace(propvalue, "<.*?>", string.Empty);
                            propvalue = Regex.Replace(propvalue,"\t", " ");
                            propvalue = Regex.Replace(propvalue,"\r", " ");
                            propvalue = Regex.Replace(propvalue,"\n", " ");
                            propvalue = propvalue.Trim(' ');
                        }

                        searchObjects.Add(new SearchObject()
                        {
                            Instance=obj,
                            Value= propvalue,
                            Power=0
                        });
                    }

                }
            }

            return searchObjects;
        }
    }

    public class SearchObject
    {
        public dynamic Instance { get; set; }
        public string Value { get; set; }
        public int Power { get; set; }
    }

    public class SearchModelProperty
    {
        public SearchModelProperty(string name, bool isHtml = false)
        {
            PropertyName = name;
            IsHtml = isHtml;
        }
        public string PropertyName { get; set; }
        public bool IsHtml { get; set; }
    }
}
