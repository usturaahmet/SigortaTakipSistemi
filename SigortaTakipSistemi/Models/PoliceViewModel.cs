using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SigortaTakipSistemi.ViewModels
{
    public class PoliceViewModel
    {
        public int? Id { get; set; }

        [Required, StringLength(50)]
        public string PoliceNo { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Kisi { get; set; } = string.Empty;

        [Required, StringLength(11, MinimumLength = 11)]
        public string TcNo { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string TelefonNo { get; set; } = string.Empty;

        [Required]
        public int SigortaSirketiId { get; set; }

        [Required]
        public int PoliceTuruId { get; set; }

        [Required]
        public int PersonelId { get; set; }

        [DataType(DataType.Date)]
        public DateTime TanzimTarihi { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; } = DateTime.Today;

        [Range(1, 120)]
        public int PoliceSuresi { get; set; } = 12;

        [Range(0, 99999999999999.99)]
        public decimal Fiyat { get; set; }

        // UI dropdown listeleri
        public List<SelectListItem> SigortaSirketleri { get; set; } = new();
        public List<SelectListItem> PoliceTurleri { get; set; } = new();
        public List<SelectListItem> Personeller { get; set; } = new();
    }
}
