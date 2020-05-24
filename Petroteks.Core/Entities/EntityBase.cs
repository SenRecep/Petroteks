using System;

namespace Petroteks.Core.Entities
{
    public class EntityBase : IEntityBase
    {
        public int id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int CreateUserid { get; set; } = 0;
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserid { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
