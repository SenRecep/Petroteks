using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface ICategory
    {
        int Parentid { get; set; }
        string Name { get; set; }
        string PhotoPath { get; set; }
    }
}
