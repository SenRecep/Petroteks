using Microsoft.EntityFrameworkCore;
using Petroteks.Entities.Concrete;

namespace Petroteks.Dal.Concreate.EntityFramework.Contexts
{
    public class PetroteksDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"workstation id=Petroteks.mssql.somee.com;packet size=4096;user id=Petroteks_SQLLogin_1;pwd=bpg54w5vnk;data source=Petroteks.mssql.somee.com;persist security info=False;initial catalog=Petroteks");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
    }
}
