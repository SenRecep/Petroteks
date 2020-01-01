using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class MainPage : BasePage, IMainPage
    {
        public string Slider { get; set; }
        public string TopContent { get; set; }
        public string BottomContent { get; set; }
    }
}
