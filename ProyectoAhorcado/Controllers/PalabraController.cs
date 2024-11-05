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
    public class PalabraController : Controller
    {
        private readonly ProyectoAhorcadoContext _context;

        public PalabraController(ProyectoAhorcadoContext context)
        {
            _context = context;
        }

        // GET: Palabras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Palabra.ToListAsync());
        }

        // GET: Palabras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabra = await _context.Palabra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palabra == null)
            {
                return NotFound();
            }

            return View(palabra);
        }

        // GET: Palabras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Palabras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto")] Palabra palabra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(palabra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(palabra);
        }

        // GET: Palabras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabra = await _context.Palabra.FindAsync(id);
            if (palabra == null)
            {
                return NotFound();
            }
            return View(palabra);
        }

        // POST: Palabras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto")] Palabra palabra)
        {
            if (id != palabra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palabra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalabraExists(palabra.Id))
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
            return View(palabra);
        }

        // GET: Palabras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabra = await _context.Palabra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palabra == null)
            {
                return NotFound();
            }

            return View(palabra);
        }

        // POST: Palabras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var palabra = await _context.Palabra.FindAsync(id);
            if (palabra != null)
            {
                _context.Palabra.Remove(palabra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalabraExists(int id)
        {
            return _context.Palabra.Any(e => e.Id == id);
        }
    }
}
