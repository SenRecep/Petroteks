using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Notice : ViewComponent
    {
        private readonly IUI_NoticeService uI_NoticeService;

        public Notice(IUI_NoticeService uI_NoticeService)
        {
            this.uI_NoticeService = uI_NoticeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notices = new List<UI_Notice>();
            foreach (var x in uI_NoticeService.GetMany(x => x.IsActive && x.WebSite == WebsiteContext.CurrentWebsite ))
                if (x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                    notices.Add(x);
            return View(notices);
        }
    }
}
