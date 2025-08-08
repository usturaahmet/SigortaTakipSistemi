using System.ComponentModel.DataAnnotations;

namespace SigortaTakipSistemi.Models
{
    public class SigortaSirketi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket adı boş bırakılamaz.")]
        public string Ad { get; set; }
    }
}