namespace ProyectoAhorcado.Models
{
    using System.Collections.Generic;

    public class Palabra
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public List<Juego> Juegos { get; set; } = new List<Juego>(); // Relación inversa
    }


}
