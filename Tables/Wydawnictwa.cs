using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Tables
{
    public class Wydawnictwa
    {
        [Key]
        public int Id_Wyd { get; set; }

        [MaxLength(40)]
        public string Nazwa { get; set; } = null!;

        [MaxLength(40)]
        public string Kraj_poch { get; set; } = null!;

        [MaxLength(35)] 
        public string Miasto { get; set; } = null!;

        public string Adres { get; set; } = null!;

        [Required]
        public int rok_zal { get; set; }

        [Required] 
        public Boolean aktywne { get; set; }

        public ICollection<Książki>? Ksiażki { get; set; }
    }
}
