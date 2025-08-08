using System.ComponentModel.DataAnnotations;

namespace SigortaTakipSistemi.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim soyisim giriniz.")]
        public string IsimSoyisim { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Eposta { get; set; }

        [Required(ErrorMessage = "Şifre giriniz.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Rol seçiniz.")]
        public string Rol { get; set; } // Admin veya Kullanici
    }
}
