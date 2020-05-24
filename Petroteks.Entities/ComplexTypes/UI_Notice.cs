using Petroteks.Entities.Abstract;
using System;

namespace Petroteks.Entities.ComplexTypes
{
    public class UI_Notice : ML_WebsiteObject, IUI_Notice
    {
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
