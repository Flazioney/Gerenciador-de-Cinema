using System;
using System.ComponentModel.DataAnnotations;
namespace Gerenciador_de_Cinema.Models
{

    public class Filmes
    {
        [Key]
        public int id_filme { get; internal set; }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime duracao { get; set; }
    }
}
