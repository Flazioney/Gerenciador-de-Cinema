using System;
using System.ComponentModel.DataAnnotations;
namespace Gerenciador_de_Cinema.Models
{
    public class Salas
    {
        [Key]
        public int id_sala { get; set; }
        public string Nome { get; set; }
        public int qtd_assentos { get; set; }
    }
}
