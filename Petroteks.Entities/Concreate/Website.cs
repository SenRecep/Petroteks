﻿using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Website : IWebsite
    {
        public string BaseUrl { get; set; }
        public string Name { get; set; }
    }
}