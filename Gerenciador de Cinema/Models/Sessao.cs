using System;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Cinema.Models
{
    public class Sessao
    {
        [Key]
        public int id_sessao { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Data de Exibição")]
        [DataType(DataType.Date)]
        public DateTime data_exb { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Inicio do Filme")]
        public TimeSpan hr_ini { get; set; }

        //[Required(ErrorMessage = "{0} required")]
        [Display(Name = "Fim do Filme")]
        public TimeSpan hr_fim { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Valor do Ingresso")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal valor_ing { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Filme")]
        public int id_filme { get; set; }


        public virtual Filmes Filmes { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Sala de Exibição")]
        public int id_sala { get; set; }
        public virtual Salas Salas { get; set; }



    }
}
