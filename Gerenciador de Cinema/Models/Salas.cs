using System.ComponentModel.DataAnnotations;
namespace Gerenciador_de_Cinema.Models
{
    public class Salas
    {
        [Key]
        public int id_sala { get; set; }

        [Display(Name = "Salas")]
        public string Nome { get; set; }

        [Display(Name = "Qtd. Assentos")]
        public int qtd_assentos { get; set; }

    }
}
