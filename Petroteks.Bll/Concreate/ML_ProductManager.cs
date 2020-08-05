using System;
using System.Collections.Generic;
using System.Text;
using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Bll.Concreate
{
    public class ML_ProductManager : EntityManager<ML_Product>, IML_ProductService
    {
        private readonly IML_ProductDal repostory;

        public ML_ProductManager(IML_ProductDal repostory) : base(repostory)
        {
            this.repostory = repostory;
        }

        public List<ML_Product> GetAllActiveLoaded()
        {
            return repostory.GetAllActiveLoaded();
        }
    }
}
