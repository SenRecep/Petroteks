using System;
using System.ComponentModel.DataAnnotations;

namespace Petroteks.MvcUi.Models
{
    public class NoticeViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
