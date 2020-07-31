using System;
using System.Collections.Generic;
using System.Text;
using Petroteks.Core.Entities;

namespace Petroteks.Bll.Abstract
{
    public interface LanguageAndWebsiteFilteredDataService<T> where T:EntityBase,new()
    {
        ICollection<T> LanguageAndWebsiteFilteredData(int websiteId,int languageId);
    }
}
