using System.ComponentModel.DataAnnotations;

namespace SigortaTakipSistemi.Models
{
    public class PoliceTuru
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Poliçe türü adı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Prim oranı zorunludur.")]
        public decimal Prim { get; set; }

        [Required(ErrorMessage = "Acenta primi zorunludur.")]
        public decimal AcentaPrimi { get; set; }
    }
}
