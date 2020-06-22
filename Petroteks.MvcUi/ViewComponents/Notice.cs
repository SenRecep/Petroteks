using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Notice : LanguageVC
    {
        private readonly IUI_NoticeService uI_NoticeService;

        public Notice(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_NoticeService = serviceProvider.GetService<IUI_NoticeService>();
        }
        public IViewComponentResult Invoke()
        {
            List<UI_Notice> notices = new List<UI_Notice>();
            foreach (UI_Notice x in uI_NoticeService.GetMany(x => x.IsActive && x.WebSite == CurrentWebsite, CurrentLanguage.id))
            {
                if (x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                {
                    notices.Add(x);
                }
            }

            return View(notices);
        }

    }
}
