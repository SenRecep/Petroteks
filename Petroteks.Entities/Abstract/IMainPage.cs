using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IMainPage
    {
        string Slider { get; set; }
        string TopContent { get; set; }
        string BottomContent { get; set; }
    }
}
