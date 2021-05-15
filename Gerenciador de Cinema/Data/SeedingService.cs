using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador_de_Cinema.Models;

namespace Gerenciador_de_Cinema.Data
{
    public class SeedingService
    {
        private Gerenciador_de_CinemaContext _CinemaContext;

        public SeedingService(Gerenciador_de_CinemaContext Context)
        {
            _CinemaContext = Context;
        }

        public void Seed()
        {
            if(_CinemaContext.Filmes.Any()
                || _CinemaContext.Salas.Any() ||
                _CinemaContext.Sessao.Any())
            {
                return;
            }
           
        }
    }
}
