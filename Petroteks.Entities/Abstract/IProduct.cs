﻿using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IProduct
    {
        int Categoryid { get; set; }
        Category Category { get; set; }
        string PhotoPath { get; set; }
        string SupTitle { get; set; }
        string SubTitle { get; set; }
    }
}