using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppCursosCast.Models;

    public class CursosContext : DbContext
    {
        public CursosContext (DbContextOptions<CursosContext> options)
            : base(options)
        {
        }

        public DbSet<AppCursosCast.Models.Categoria>? Categoria { get; set; }

        public DbSet<AppCursosCast.Models.Curso>? Curso { get; set; }

        public DbSet<AppCursosCast.Models.Log>? Log { get; set; }
    }
