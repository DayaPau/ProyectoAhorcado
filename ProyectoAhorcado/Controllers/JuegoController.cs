using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoAhorcado.Data;
using ProyectoAhorcado.Models;

namespace ProyectoAhorcado.Controllers
{
    public class JuegoController : Controller
    {
        private readonly ProyectoAhorcadoContext _context;

        public JuegoController(ProyectoAhorcadoContext context)
        {
            _context = context;
        }

        // GET: Juegoes
        public async Task<IActionResult> Index()
        {
            var proyectoAhorcadoContext = _context.Juego.Include(j => j.Palabra);
            return View(await proyectoAhorcadoContext.ToListAsync());
        }

        // GET: Juegoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juego
                .Include(j => j.Palabra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // GET: Juegoes/Create
        public IActionResult Create()
        {
            ViewData["PalabraId"] = new SelectList(_context.Palabra, "Id", "Id");
            return View();
        }

        // POST: Juegoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PalabraOculta,IntentosRestantes,PalabraId")] Juego juego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(juego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PalabraId"] = new SelectList(_context.Palabra, "Id", "Id", juego.PalabraId);
            return View(juego);
        }

        // GET: Juegoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juego.FindAsync(id);
            if (juego == null)
            {
                return NotFound();
            }
            ViewData["PalabraId"] = new SelectList(_context.Palabra, "Id", "Id", juego.PalabraId);
            return View(juego);
        }

        // POST: Juegoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PalabraOculta,IntentosRestantes,PalabraId")] Juego juego)
        {
            if (id != juego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(juego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuegoExists(juego.Id))
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
            ViewData["PalabraId"] = new SelectList(_context.Palabra, "Id", "Id", juego.PalabraId);
            return View(juego);
        }

        // GET: Juegoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juego
                .Include(j => j.Palabra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // POST: Juegoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var juego = await _context.Juego.FindAsync(id);
            if (juego != null)
            {
                _context.Juego.Remove(juego);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JuegoExists(int id)
        {
            return _context.Juego.Any(e => e.Id == id);
        }
    }
}
