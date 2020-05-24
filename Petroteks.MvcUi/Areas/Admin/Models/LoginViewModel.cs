using System.ComponentModel.DataAnnotations;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen email bilgisini doğru giriniz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }




        [Required(ErrorMessage = "Lütfen şifre bilgisini doğru giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }





        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
