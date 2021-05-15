using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador_de_Cinema.Models;

namespace Gerenciador_de_Cinema.Controllers
{
    public class FilmesController : Controller
    {
        public IActionResult Index()
        {
            List<Filmes> list = new List<Filmes>();
            list.Add(new Filmes { id_filme = 1, Titulo = "A volta dos que não foram", Descricao = "Baseado em realidade alternativa da verdade absoluta", duracao = "03:00" });
            list.Add(new Filmes { id_filme = 2, Titulo = "As tranças de um careca", Descricao = "Baseado em fios de cabelos coloridos", duracao = "02:00" });



            return View(list);
        }
    }
}
