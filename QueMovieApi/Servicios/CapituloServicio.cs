using Contexto;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public interface ICapituloServicio
    {
        IEnumerable<Capitulo> MostrarTodo();
        IEnumerable<Capitulo> MostrarTodo(int id);
        Capitulo MostrarUno(int id);
        bool Agregar(Capitulo modelo);
        bool Modificar(Capitulo modelo);
        bool Eliminar(int id);
    }

    public class CapituloServicio : ICapituloServicio
    {
        //Dependencia
        private readonly ApiDbContext _capituloDbContext;

        public CapituloServicio(ApiDbContext capituloDbContext)
        {
            _capituloDbContext = capituloDbContext;
        }

        public bool Agregar(Capitulo modelo)
        {
            try
            {
                _capituloDbContext.Add(modelo);
                _capituloDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Capitulo> MostrarTodo()
        {
            var resultado = new List<Capitulo>();

            try
            {
                resultado = _capituloDbContext.Capitulo.ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public IEnumerable<Capitulo> MostrarTodo(int id)
        {
            var resultado = new List<Capitulo>();

            try
            {
                resultado = _capituloDbContext.Capitulo.Where(x => x.SerieId == id).ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }
        
        public Capitulo MostrarUno(int id)
        {
            var resultado = new Capitulo();

            try
            {
                resultado = _capituloDbContext.Capitulo.Single(x => x.CapituloID == id);
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public bool Modificar(Capitulo modelo)
        {
            try
            {
                var modeloOriginal = _capituloDbContext.Capitulo.Single(x => x.CapituloID == modelo.CapituloID);
                
                modeloOriginal.NumeroCapitulo = modelo.NumeroCapitulo;
                modeloOriginal.NombreDelCapitulo = modelo.NombreDelCapitulo;
                modeloOriginal.DireccionDelCapitulo = modelo.DireccionDelCapitulo;
                modeloOriginal.Temporada = modelo.Temporada;
                modeloOriginal.SerieId = modelo.SerieId;

                _capituloDbContext.Update(modeloOriginal);
                _capituloDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Eliminar(int id)
        {
            try
            {
                Capitulo capitulo = (Capitulo)_capituloDbContext.Capitulo.Where(x => x.CapituloID == id).First();
                _capituloDbContext.Capitulo.Remove(capitulo);
                _capituloDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
