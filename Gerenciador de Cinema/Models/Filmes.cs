using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_Cinema.Models
{
    public class Filmes
    {
        public int id_filme { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string duracao { get; set; }
    }
}
