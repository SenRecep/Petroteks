using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Areas.Admin.Data
{
    public static class DataTransferPoint
    {
        public static ICollection<Email> SelectedEmails { get; set; }
    }
}
