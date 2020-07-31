using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;
namespace Petroteks.Bll.Concreate
{
    public class BlogManager : EntityManager<Blog>, IBlogService
    {
        public BlogManager(IBlogDal repostory) : base(repostory)
        {
        }
        public override Blog Get(Expression<Func<Blog, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<Blog> GetMany(Expression<Func<Blog, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        public Blog GetAllLanguageBlog(Expression<Func<Blog, bool>> filter, params string[] navigations)
        {
            return base.Get(filter, navigations);
        }

        public ICollection<Blog> LanguageAndWebsiteFilteredData(int websiteId, int languageId)
        {
            return base.GetMany(x => x.IsActive && x.WebSiteid == websiteId && x.Languageid == languageId);
        }
    }
}

