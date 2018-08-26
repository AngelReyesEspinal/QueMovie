using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Modelo;

namespace Contexto
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Capitulo> Capitulo { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<UsuarioSerie> UsuarioSerie { get; set; }
        public DbSet<GeneroSerie> GeneroSerie { get; set; }
        public DbSet<UsuarioCapitulo> UsuarioCapitulo { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Usuario
            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Nombre)
                        .HasMaxLength(40)
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Apellido)
                        .HasMaxLength(80)
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.NombreCuenta)
                        .HasMaxLength(80)
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Contrasenia)
                        .HasMaxLength(80)
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Correo)
                        .HasMaxLength(50)
                        .IsRequired();
            
            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Sexo)
                        .HasMaxLength(10)
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.Tipo)
                        .HasMaxLength(15)  
                        .IsRequired();

            modelBuilder.Entity<Usuario>()
                        .Property(x => x.PreguntaDeSeguridad)
                        .HasMaxLength(30);
            #endregion

            #region Serie
            modelBuilder.Entity<Serie>()
                        .HasMany(s => s.Capitulos)
                        .WithOne(c => c.Serie)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Titulo)
                        .HasMaxLength(80)
                        .IsRequired();
            
            modelBuilder.Entity<Serie>()
                        .Property(x => x.Sinopsis)
                        .HasColumnType("text")
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Anio)
                        .HasMaxLength(10)
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Productora)
                        .HasMaxLength(50)
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Duracion)
                        .HasMaxLength(50) //50 minutos
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Imagen)
                        .HasMaxLength(100)
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Trailer)
                        .HasMaxLength(120)
                        .IsRequired();

            modelBuilder.Entity<Serie>()
                        .Property(x => x.Temporadas)
                        .HasMaxLength(100)
                        .IsRequired();
            #endregion

            #region Capitulo
            //modelBuilder.Entity<Capitulo>()
            //            .HasOne(c => c.Serie)
            //            .WithMany(a => a.Capitulos)
            //            .IsRequired()
            //            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Capitulo>()
                        .Property(x => x.NumeroCapitulo)
                        .IsRequired();

            modelBuilder.Entity<Capitulo>()
                        .Property(x => x.NombreDelCapitulo)
                        .HasMaxLength(80)
                        .IsRequired();

            modelBuilder.Entity<Capitulo>()
                        .Property(x => x.DireccionDelCapitulo)
                        .HasMaxLength(150)
                        .IsRequired();

            modelBuilder.Entity<Capitulo>()
                        .Property(x => x.SerieId)
                        .IsRequired();

            modelBuilder.Entity<Capitulo>()
                        .Property(x => x.Temporada)
                        .HasMaxLength(30)
                        .IsRequired();
            #endregion

            #region Genero
            modelBuilder.Entity<Genero>()
                        .Property(x => x.Generos)
                        .HasMaxLength(100)
                        .IsRequired();
            #endregion

            #region Comentarios

            modelBuilder.Entity<UsuarioCapitulo>()
                        .Property(x => x.Comentario)
                        .HasColumnType("text");

            #endregion
        }

    }
}
