using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Tables
{
    public class Autorzy
    {
        [Key]
        public int Id_Autora { get; set; }

        [MaxLength(20)]
        public string Imię { get; set; } = null!;

        [MaxLength(30)] 
        public string Nazwisko { get; set; } = null!;

        [MaxLength(20)] 
        public string Pochodzenie { get; set; } = null!;

        public ICollection<Książki>? Ksiażki { get; set; }

    }
}
