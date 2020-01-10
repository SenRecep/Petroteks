using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IEmail
    {
        string Category { get; set; }
        string EmailAddress { get; set; }
    }
}
