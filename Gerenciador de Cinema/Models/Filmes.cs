using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace Gerenciador_de_Cinema.Models
{

    public class Filmes
    {
        [Key]
        public int id_filme { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Duração Filme")]
        [DataType(DataType.Time)]
        public DateTime duracao { get; set; }

        public ICollection<Filmes> Filmess { get; set; } = new List<Filmes>();

        public Filmes()
        {

        }

        public Filmes(int Id_filme,string TTitulo, string DDescricao, DateTime Duracao)
        {
            //Id_filme = id_filme;
            TTitulo = Titulo;
            DDescricao = Descricao;
            Duracao = duracao;
        }
        public void AddFilme(Filmes filmes)
        {
            Filmess.Add(filmes);
        }

    }
}
