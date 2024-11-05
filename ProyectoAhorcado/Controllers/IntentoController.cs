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
    public class IntentoController : Controller
    {
        private readonly ProyectoAhorcadoContext _context;

        public IntentoController(ProyectoAhorcadoContext context)
        {
            _context = context;
        }

        // GET: Intentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Intento.ToListAsync());
        }

        // GET: Intentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intento = await _context.Intento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intento == null)
            {
                return NotFound();
            }

            return View(intento);
        }

        // GET: Intentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Intentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Letra,Correcto")] Intento intento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(intento);
        }

        // GET: Intentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intento = await _context.Intento.FindAsync(id);
            if (intento == null)
            {
                return NotFound();
            }
            return View(intento);
        }

        // POST: Intentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Letra,Correcto")] Intento intento)
        {
            if (id != intento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntentoExists(intento.Id))
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
            return View(intento);
        }

        // GET: Intentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intento = await _context.Intento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intento == null)
            {
                return NotFound();
            }

            return View(intento);
        }

        // POST: Intentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intento = await _context.Intento.FindAsync(id);
            if (intento != null)
            {
                _context.Intento.Remove(intento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntentoExists(int id)
        {
            return _context.Intento.Any(e => e.Id == id);
        }
    }
}
