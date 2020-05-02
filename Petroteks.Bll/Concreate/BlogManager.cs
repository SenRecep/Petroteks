﻿using static Petroteks.Bll.Helpers.LanguageContext;
using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Petroteks.Bll.Concreate
{
    public class BlogManager : EntityManager<Blog>, IBlogService
    {
        public BlogManager(IBlogDal repostory) : base(repostory)
        {
        }
        public override Blog Get(Expression<Func<Blog, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<Blog> GetMany(Expression<Func<Blog, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}
