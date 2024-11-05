using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAhorcado.Models
{
    public class Intento
    {
        public int Id { get; set; }
        public char Letra { get; set; }
        public bool Correcto { get; set; }
       
        public int JuegoId { get; set; } // Clave foránea


        public Juego Juego { get; set; } // Navegación al Juego
    }

}
