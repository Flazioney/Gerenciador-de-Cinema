using System;
using System.ComponentModel.DataAnnotations;
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
        //[DataType(DataType.Time)]
        public TimeSpan duracao { get; set; }

        /*
        public Filmes()
        {

        }

        public Filmes(int id, string titulo, string descricao, TimeSpan dura)
        {
            id = id_filme;
            titulo = Titulo;
            Descricao = descricao;
            duracao = dura;
        }
        */
    }
}
