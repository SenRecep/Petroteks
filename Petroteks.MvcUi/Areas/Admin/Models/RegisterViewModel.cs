using System.ComponentModel.DataAnnotations;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı bilgisini lütfen doğru giriniz")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ad bilgisini lütfen doğru giriniz")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Soyad bilgisini lütfen doğru giriniz")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Şifre bilgisini lütfen doğru giriniz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "İki şifre birbiriyle uyuşmamaktadır.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirmation { get; set; }
        [Required(ErrorMessage = "Email Adresini lütfen doğru giriniz.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
