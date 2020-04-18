using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.Bll.Concreate
{
    public class EmailManager : EntityManager<Email>, IEmailService { public EmailManager(IEmailDal repostory) : base(repostory) { } }
}
