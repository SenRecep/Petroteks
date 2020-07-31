using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Controllers
{
    public class SearchController : GlobalController
    {
        private readonly SearchManager searchManager;
        private readonly IServiceProvider serviceProvider;

        public SearchController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.searchManager = serviceProvider.GetService<SearchManager>();
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public IActionResult Index(string s)
        {
            searchManager.Build(base.CurrentWebsiteId, base.CurrentLanguageId);
            var result = searchManager.Search(s);
            return View(new SearchViewModel()
            {
                SearchKey = s,
                Result = result
            }) ;
        }
    }
}
