using ProyectoAhorcado.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAhorcado.Models
{
    public class Juego
    {
        [Key]
        public int Id { get; set; }
        public string PalabraOculta { get; set; }
        public List<Intento> Intentos { get; set; } = new List<Intento>();
        public int IntentosRestantes { get; set; }

        public int PalabraId { get; set; } // Clave foránea

        [ForeignKey("PalabraId")]
        public Palabra Palabra { get; set; } // Navegación a la Palabra
    }
}
