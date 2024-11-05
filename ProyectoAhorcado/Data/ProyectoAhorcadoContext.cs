using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoAhorcado.Models;

namespace ProyectoAhorcado.Data
{
    public class ProyectoAhorcadoContext : DbContext
    {
        public ProyectoAhorcadoContext (DbContextOptions<ProyectoAhorcadoContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoAhorcado.Models.Juego> Juego { get; set; } = default!;
        public DbSet<ProyectoAhorcado.Models.Intento> Intento { get; set; } = default!;
        public DbSet<ProyectoAhorcado.Models.Palabra> Palabra { get; set; } = default!;
    }
}
