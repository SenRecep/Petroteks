using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Areas.Admin.Data
{
    public static class DataTransferPoint
    {
        public static ICollection<Email> SelectedEmails{ get; set; }
    }
}
