using Gerenciador_de_Cinema.Data;
using Gerenciador_de_Cinema.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Gerenciador_de_Cinema.Controllers
{
    public class FilmesController : Controller
    {
        private readonly Gerenciador_de_CinemaContext _context;
        private readonly IAutenticacao _autentica;

        public FilmesController(Gerenciador_de_CinemaContext context, IAutenticacao autentica)
        {
            _context = context;
            _autentica = autentica;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filmes.ToListAsync());
            List<int> filmes = _context.Filmes.Select(m => m.id_filme).ToList();
            return View(filmes);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes
                .FirstOrDefaultAsync(m => m.id_filme == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(Filmes filmes, IFormFile Img, [FromServices] Gerenciador_de_CinemaContext db)
        {
                
                filmes.Dados = Img.ToByteArray();
                filmes.Length = (int)Img.Length;
                filmes.Extension = Img.GetExtension();
                filmes.ContentType = Img.ContentType;
                db.Filmes.Add(filmes);
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }


        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public FileResult Render(int id, [FromServices] Gerenciador_de_CinemaContext db)
        {            

            var item = db.Filmes
                .Where(x => x.id_filme == id)
                .Select(s => new { s.Dados, s.ContentType })
                .FirstOrDefault();

            if (item != null)
            {
                return File(item.Dados, item.ContentType);
            }

            return null;
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes.FindAsync(id);
            if (filmes == null)
            {
                return NotFound();
            }
            return View(filmes);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Titulo,Descricao,duracao,Dados,ContentType")] Filmes filmes, IList<IFormFile> Dados)
        {
            if (id != filmes.id_filme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IFormFile imagemEnviada = Dados.FirstOrDefault();
                    if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
                    {
                        MemoryStream ms = new MemoryStream();
                        imagemEnviada.OpenReadStream().CopyTo(ms);
                        Filmes imagemEntity = new Filmes()
                        {

                            Titulo = imagemEnviada.Name,
                            Dados = ms.ToArray(),
                            ContentType = imagemEnviada.ContentType
                        };

                        var q = _autentica.UpdateFilme(filmes);

                        TempData["Erro"] = "Não pode ser excluido com menos de 10 dias id sessão " + q;
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmesExists(filmes.id_filme))
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
            return View(filmes);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes
                .FirstOrDefaultAsync(m => m.id_filme == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);
            _context.Filmes.Remove(filmes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmesExists(int id)
        {
            return _context.Filmes.Any(e => e.id_filme == id);
        }
        //post na imagem
        //[HttpPost]
        //public IActionResult UploadImagem(IList<IFormFile> arquivos)
        //{
        //    IFormFile imagemEnviada = arquivos.FirstOrDefault();
        //    if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        imagemEnviada.OpenReadStream().CopyTo(ms);
        //        Filmes imagemEntity = new Filmes()
        //        {
        //            Titulo = imagemEnviada.Name,
        //            Dados = ms.ToArray(),
        //            ContentType = imagemEnviada.ContentType
        //        };
        //        _context.Filmes.Add(imagemEntity);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        //public FileStreamResult VerImagem(int id)
        //{
        //    Filmes filmes = _context.Filmes.FirstOrDefault(m => m.id_filme == id);
        //    MemoryStream ms = new MemoryStream(filmes.Dados);
        //    return new FileStreamResult(ms, filmes.ContentType);
        //}
    }
}
