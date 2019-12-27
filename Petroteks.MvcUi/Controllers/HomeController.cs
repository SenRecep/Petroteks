using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Core.Dal;
using Petroteks.Entities.Concrete;
using System;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            #region Add Test
            //User user = new User()
            //{
            //    Email="67rsen00@gmail.com",
            //    Firstname="Recep",
            //    Lastname="Şen",
            //    Password="Sifrelenmemis Sifre Test",
            //    Role=0,
            //    TagName="Daniga",
            //};
            //_userService.Add(user);
            //ViewBag.SaveStatus = _userService._Save(); 
            #endregion
            return View(_userService.GetAll());
        }
    }
}
