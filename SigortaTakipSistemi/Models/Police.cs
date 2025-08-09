using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SigortaTakipSistemi.Models
{
    public class Police
    {
        public int Id { get; set; }

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
        public DateTime TanzimTarihi { get; set; }

        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [Range(1, 120)]
        public int PoliceSuresi { get; set; } // ay

        [Precision(18, 2)]
        public decimal Fiyat { get; set; }

        // 🔹 DB’de yok, sadece C# tarafında hesaplanıyor
        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi => BaslangicTarihi.AddMonths(PoliceSuresi);

        // Navigation properties
        public virtual SigortaSirketi? SigortaSirketi { get; set; }
        public virtual PoliceTuru? PoliceTuru { get; set; }
        public virtual Kullanici? Personel { get; set; }
    }
}
