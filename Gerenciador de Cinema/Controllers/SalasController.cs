﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Cinema.Data;
using Gerenciador_de_Cinema.Models;

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

        // GET: Salas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_sala,Nome,qtd_assentos")] Salas salas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salas);
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas = await _context.Salas.FindAsync(id);
            if (salas == null)
            {
                return NotFound();
            }
            return View(salas);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_sala,Nome,qtd_assentos")] Salas salas)
        {
            if (id != salas.id_sala)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalasExists(salas.id_sala))
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
            return View(salas);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salas = await _context.Salas.FindAsync(id);
            _context.Salas.Remove(salas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalasExists(int id)
        {
            return _context.Salas.Any(e => e.id_sala == id);
        }
    }
}
