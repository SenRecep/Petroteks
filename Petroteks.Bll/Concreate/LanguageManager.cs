using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.Bll.Concreate
{
    public class LanguageManager : EntityManager<Language>, ILanguageService
    {
        public LanguageManager(ILanguageDal repostory) : base(repostory)
        {
        }
    }
}
