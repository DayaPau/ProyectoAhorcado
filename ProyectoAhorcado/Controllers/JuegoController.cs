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
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class JuegoController : Controller
    {
        private readonly ProyectoAhorcadoContext _context;

        public JuegoController(ProyectoAhorcadoContext context)
        {
            _context = context;
        }

        // Método para iniciar un nuevo juego
        public IActionResult Index()
        {
            var juego = new Juego
            {
                PalabraOculta = ObtenerPalabraAleatoria(), // Método que obtendrá una palabra aleatoria
                Intentos = new List<Intento>(),
                IntentosRestantes = 6 // Por ejemplo, 6 intentos
            };

            _context.Juego.Add(juego);
            _context.SaveChanges();

            return RedirectToAction("Juego", new { id = juego.Id });
        }

        // Método para mostrar el estado del juego
        public IActionResult Juego(int id)
        {
            var juego = _context.Juego.Include(j => j.Intentos).FirstOrDefault(j => j.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego); // Retorna la vista con el modelo del juego
        }

        // Método para hacer un intento
        [HttpPost]
        public IActionResult Intentar(int id, char letra)
        {
            var juego = _context.Juego.Include(j => j.Intentos).FirstOrDefault(j => j.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            bool esCorrecto = juego.PalabraOculta.Contains(letra);
            juego.Intentos.Add(new Intento { Letra = letra, Correcto = esCorrecto });

            if (!esCorrecto)
            {
                juego.IntentosRestantes--;
            }

            // Verificar si el juego ha terminado
            if (juego.IntentosRestantes <= 0)
            {
                // Lógica para manejar el final del juego (perdido)
                // Aquí puedes redirigir a una vista de "Perdiste"
            }
            else if (JuegoGanado(juego)) // Método que verifica si el jugador ha ganado
            {
                // Lógica para manejar el final del juego (ganado)
                // Aquí puedes redirigir a una vista de "Ganaste"
            }

            _context.SaveChanges();

            return RedirectToAction("Juego", new { id = juego.Id });
        }

        // Método auxiliar para obtener una palabra aleatoria
        private string ObtenerPalabraAleatoria()
        {
            // Aquí debes implementar la lógica para obtener una palabra de la base de datos o una lista de palabras.
            return _context.Palabra.OrderBy(p => Guid.NewGuid()).FirstOrDefault()?.Texto;
        }

        // Método auxiliar para verificar si el jugador ha ganado
        private bool JuegoGanado(Juego juego)
        {
            foreach (var letra in juego.PalabraOculta)
            {
                if (!juego.Intentos.Any(i => i.Letra == letra))
                {
                    return false; // Si falta alguna letra, el juego no está ganado
                }
            }
            return true; // Todas las letras han sido adivinadas
        }
    }

}
