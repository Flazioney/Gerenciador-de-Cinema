﻿using Gerenciador_de_Cinema.Data;
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

        public FilmesController(Gerenciador_de_CinemaContext context)
        {
            _context = context;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_filme,Titulo,Descricao,duracao")] Filmes filmes, IFormFile upload)
        {
           
            if (ModelState.IsValid)
            {
                //if (upload != null && upload.Length > 0)
                //{
                //    var arqImagem = new Filmes
                //    {
                //        ContentType = upload.ContentType
                //    };
                //    var reader = new BinaryReader(upload.OpenReadStream());
                //    arqImagem.Dados = reader.ReadBytes((int)upload.Length);
                //    filmes.Dados = arqImagem.Dados;
                //    filmes.ContentType = arqImagem.ContentType;
                    _context.Add(filmes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

               

                TempData["mensagem"] = string.Format("{0}  : foi incluído com sucesso", filmes.Titulo);
                return RedirectToAction("filmes");
            }
            return View(filmes);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_filme,Titulo,Descricao,duracao,Dados")] Filmes filmes)
        {
            if (id != filmes.id_filme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmes);
                    await _context.SaveChangesAsync();
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
