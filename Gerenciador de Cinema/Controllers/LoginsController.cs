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
    public class LoginsController : Controller
    {
        private readonly Gerenciador_de_CinemaContext _context;

        private readonly IAutenticacao _autentica;

        public LoginsController(IAutenticacao autentica, Gerenciador_de_CinemaContext context)
        {
            _autentica = autentica;
            _context = context;
        }

        //public LoginsController(Gerenciador_de_CinemaContext context)
        //{
        //    _context = context;
        //}

        // GET: Logins
        public async Task<IActionResult> Index()
        {

            var sessao = await _context.Sessao.ToListAsync();
            var salas = await _context.Salas.ToListAsync();
            var filmes = await _context.Filmes.ToListAsync();
            return View();


        }

        public async Task<IActionResult> Cartaz()
        {
            var sessao = await _context.Sessao.ToListAsync();
            var salas = await _context.Salas.ToListAsync();
            var filmes = await _context.Filmes.ToListAsync();

            return View(sessao);
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.id_login == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_login,nome,senha,email,tipoAcesso")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Filmes()
        {
            return RedirectToAction("Index", "Filmes");
        }
        [HttpGet]
        public IActionResult Salas()
        {
            return RedirectToAction("Index", "Salas");
        }
        [HttpGet]
        public IActionResult Sessoes()
        {
            return RedirectToAction("Index", "Sessaos");
        }

        [HttpGet]
        public IActionResult Menu()
        {
            return RedirectToAction(nameof(Cartaz));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Menu([Bind] Login login)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                string LoginStatus = _autentica.ValidarLogin(login);


                if (LoginStatus == "Sucesso")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.email)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    if (User.Identity.IsAuthenticated)
                        return RedirectToAction(nameof(Cartaz));
                    else
                    {
                        TempData["LoginFalhou"] = "O login Falhou. Informe as credenciais corretas " + User.Identity.Name;
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["LoginFalhou"] = "O login Falhou. Informe as credenciais corretas";
                    var sessao = await _context.Sessao.ToListAsync();
                    var salas = await _context.Salas.ToListAsync();
                    var filmes = await _context.Filmes.ToListAsync();

                    return View(sessao);
                }
            }
            else
            {
                var sessao = await _context.Sessao.ToListAsync();
                var salas = await _context.Salas.ToListAsync();
                var filmes = await _context.Filmes.ToListAsync();

                return View(sessao);
            }


        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_login,nome,senha,email,tipoAcesso")] Login login)
        {
            if (id != login.id_login)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.id_login))
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
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.id_login == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.FindAsync(id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.id_login == id);
        }
        private bool SessaoExists(int id)
        {
            return _context.Sessao.Any(e => e.id_sessao == id);
        }

    }
}
