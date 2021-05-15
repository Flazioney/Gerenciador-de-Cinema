using System;
using System.ComponentModel.DataAnnotations;
namespace Gerenciador_de_Cinema.Models
{
    public class Sessao
    {
        [Key]
        public int id_sessao { get; set; }
        public DateTime data_exb { get; set; }
        public DateTime hr_ini { get; set; }
        public DateTime hr_fim { get; set; }
        public decimal valor_ing { get; set; }
        public int id_filme { get; set; }
        public int id_sala { get; set; }
    }
}
