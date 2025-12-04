using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zoologico.Modelos;

    public class ZoologicoApiContext : DbContext
    {
        public ZoologicoApiContext (DbContextOptions<ZoologicoApiContext> options)
            : base(options)
        {
        }

        public DbSet<Zoologico.Modelos.Animal> Animal { get; set; } = default!;

public DbSet<Zoologico.Modelos.Especie> Especie { get; set; } = default!;

public DbSet<Zoologico.Modelos.Raza> Raza { get; set; } = default!;
    }
