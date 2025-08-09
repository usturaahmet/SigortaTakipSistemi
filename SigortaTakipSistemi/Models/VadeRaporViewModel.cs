namespace SigortaTakipSistemi.Models.ViewModels
{
    public class PoliceVadeDto
    {
        public int Id { get; set; }
        public string PoliceNo { get; set; } = "";
        public string Kisi { get; set; } = "";
        public string? TcNo { get; set; }
        public string? TelefonNo { get; set; }

        public string SigortaSirketi { get; set; } = "";
        public string PoliceTuru { get; set; } = "";

        public DateTime Tanzim { get; set; }
        public DateTime Baslangic { get; set; }
        public int SureAy { get; set; }
        public DateTime Bitis { get; set; }
        public int KalanGun { get; set; }
        public string Durum { get; set; } = ""; // "Süresi Doldu" | "Yaklaşıyor"
        public decimal Fiyat { get; set; }
    }

    public class VadeRaporViewModel
    {
        public int Gun { get; set; }
        public List<PoliceVadeDto> Dolanlar { get; set; } = new();
        public List<PoliceVadeDto> Yaklasanlar { get; set; } = new();
        public int ToplamDolan => Dolanlar.Count;
        public int ToplamYaklasan => Yaklasanlar.Count;
    }
}
