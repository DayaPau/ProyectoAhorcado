using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAhorcado.Models
{
    public class Intento
    {
        public int Id { get; set; }
        public char Letra { get; set; }
        public bool Correcto { get; set; }
        [Key]
        public int JuegoId { get; set; } // Clave foránea

        [ForeignKey("Juego")]
        public Juego Juego { get; set; } // Navegación al Juego
    }

}
