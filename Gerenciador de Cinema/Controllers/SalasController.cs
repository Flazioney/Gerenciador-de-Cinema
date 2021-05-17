using Gerenciador_de_Cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gerenciador_de_Cinema.Controllers
{
    public class SalasController : Controller
    {
        private readonly Gerenciador_de_CinemaContext _context;

        public SalasController(Gerenciador_de_CinemaContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salas.ToListAsync());
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas = await _context.Salas
                .FirstOrDefaultAsync(m => m.id_sala == id);
            if (salas == null)
            {
                return NotFound();
            }

            return View(salas);
        }

    }

}
