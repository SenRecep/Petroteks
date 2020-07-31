using Microsoft.EntityFrameworkCore;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;

namespace Petroteks.Dal.Concreate.EntityFramework.Contexts
{
    public class PetroteksDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"workstation id=Petroteks.mssql.somee.com;packet size=4096;user id=Petroteks_SQLLogin_1;pwd=bpg54w5vnk;data source=Petroteks.mssql.somee.com;persist security info=False;initial catalog=Petroteks");
            optionsBuilder.UseSqlServer(@"Data Source=192.185.7.120;Initial Catalog=nserkang_PetroteksDb;User ID=nserkang_dbuser;Password=1Parola1");
            //optionsBuilder.UseSqlServer(@"Data Source=DANIGA-PC\SQLEXPRESS;Initial Catalog=Petroteks;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<AboutUsObject> AboutUsObjects { get; set; }
        public DbSet<PrivacyPolicyObject> PrivacyPolicyObjects { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<DynamicPage> DynamicPages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UI_Navbar> UI_Navbars { get; set; }
        public DbSet<UI_Contact> UI_Contacts { get; set; }
        public DbSet<UI_Footer> UI_Footers { get; set; }
        public DbSet<UI_Notice> UI_Notices { get; set; }
    }
}
