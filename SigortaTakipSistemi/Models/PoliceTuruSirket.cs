namespace SigortaTakipSistemi.Models
{
    public class PoliceTuruSirket
    {
        public int Id { get; set; }
        public int SigortaSirketiId { get; set; }
        public SigortaSirketi SigortaSirketi { get; set; }

        public int PoliceTuruId { get; set; }
        public PoliceTuru PoliceTuru { get; set; }

        public decimal PrimYuzde { get; set; }
        public decimal AcentaPrimiYuzde { get; set; }
        public bool Aktif { get; set; } = true;
    }


}
