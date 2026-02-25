using EF_lab4.Tables;

namespace EF_lab4.Services
{
    internal class BookModifyParams
    {
        public int? WydawnictwoId { get; set; }
        public int? AutorId { get; set; }
        public string? Tytul { get; set; }
        public stan? Stan_ks { get; set; }
        public decimal? Cena { get; set; }
    }
}
