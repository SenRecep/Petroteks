using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Dal.Concreate.EntityFramework.Mapping
{
    public class MLProductMap : IEntityTypeConfiguration<ML_Product>
    {
        public void Configure(EntityTypeBuilder<ML_Product> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x=>x.ML_Products).HasForeignKey(x=>x.ProductId);
        }
    }
}
