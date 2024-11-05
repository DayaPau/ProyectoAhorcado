using System.Collections.Generic;

namespace ProyectoAhorcado.Models
{
    public class Palabra
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public List<Juego> Juegos { get; set; } = new List<Juego>(); // Relación inversa
    }
}
