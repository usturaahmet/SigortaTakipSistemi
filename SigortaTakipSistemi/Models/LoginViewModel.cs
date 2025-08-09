// ViewModels/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace SigortaTakipSistemi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı veya e-posta zorunludur.")]
        [Display(Name = "Kullanıcı adı veya e-posta")]
        public string KullaniciAdiOrEposta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; } = string.Empty;

        [Display(Name = "Beni hatırla")]
        public bool BeniHatirla { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
