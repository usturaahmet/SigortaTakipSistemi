using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigortaTakipSistemi.Models
{
    public class Police
    {
        public int Id { get; set; }

        public string PoliceNo { get; set; }
        public string Kisi { get; set; }
        public string TcNo { get; set; }
        public string TelefonNo { get; set; }

        [Required]
        public int SigortaSirketiId { get; set; }
        public SigortaSirketi SigortaSirketi { get; set; }

        [Required]
        public int PoliceTuruId { get; set; }
        public PoliceTuru PoliceTuru { get; set; }

        [Required]
        public int PersonelId { get; set; }
        public Kullanici Personel { get; set; }

        public DateTime TanzimTarihi { get; set; }
        public DateTime BaslangicTarihi { get; set; }

        public int PoliceSuresi { get; set; } // Ay cinsinden
        public decimal Fiyat { get; set; }

        [NotMapped]
        public DateTime BitisTarihi => BaslangicTarihi.AddMonths(PoliceSuresi);
    }
}
