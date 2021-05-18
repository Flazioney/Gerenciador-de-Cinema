using Gerenciador_de_Cinema.Data;
using Gerenciador_de_Cinema.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Gerenciador_de_Cinema.Controllers
{
    public class SessaosController : Controller
    {
        private readonly Gerenciador_de_CinemaContext _context;


        private readonly IAutenticacao _autentica;

        public SessaosController(IAutenticacao autentica, Gerenciador_de_CinemaContext context)
        {
            _autentica = autentica;
            _context = context;
        }

       // GET: Sessaos
        public async Task<IActionResult> Index()
        {
            var sessao = await _context.Sessao.ToListAsync();
            var salas = await _context.Salas.ToListAsync();
            var filmes = await _context.Filmes.ToListAsync();

            return View(sessao);
        }

        // GET: Sessaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sessao = await _context.Sessao
               .FirstOrDefaultAsync(m => m.id_sessao == id);
            var salas = await _context.Salas
                 .FirstOrDefaultAsync(m => m.id_sala == id);
            var filmes = await _context.Filmes
                 .FirstOrDefaultAsync(m => m.id_filme == id);
            if (sessao == null)
            {
                return NotFound();
            }

            return View(sessao);
        }

        // GET: Sessaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sessaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_sessao,data_exb,hr_ini,hr_fim,valor_ing,id_filme,id_sala")] Sessao sessao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sessao);
        }

        // GET: Sessaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessao = await _context.Sessao.FindAsync(id);
            if (sessao == null)
            {
                return NotFound();
            }
            return View(sessao);
        }

        // POST: Sessaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_sessao,data_exb,hr_ini,hr_fim,valor_ing,id_filme,id_sala")] Sessao sessao)
        {
            if (id != sessao.id_sessao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessaoExists(sessao.id_sessao))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sessao);
        }

        // GET: Sessaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var sessao = await _context.Sessao
                    .FirstOrDefaultAsync(m => m.id_sessao == id);
            
            if (sessao == null)
            {
                return NotFound();
            }

            return View(sessao);


        }

        // POST: Sessaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            string login = _autentica.DeletarSessoes(id);
            await HttpContext.SignInAsync(login);


            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private bool SessaoExists(int id)
        {
            return _context.Sessao.Any(e => e.id_sessao == id);
        }
    }
}
