using Contexto;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{

    public interface IUsuarioSerieServicio
    {
        bool Agregar(UsuarioSerie modelo);
        List<UsuarioSerie> MostrarTodo();
        UsuarioSerie MostrarUno(UsuarioSerie modelo);
        List<UsuarioSerie> MostrarSeriesDeUnUsuario(int id);
        bool Eliminar(int id);
    }

    public class UsuarioSerieServicio : IUsuarioSerieServicio
    {
        //Dependencia
        private readonly ApiDbContext _usuarioSerieDbContext;

        public UsuarioSerieServicio(ApiDbContext usuarioSerieDbContext)
        {
            _usuarioSerieDbContext = usuarioSerieDbContext;
        }
        
        public List<UsuarioSerie> MostrarTodo()
        {
            var resultado = new List<UsuarioSerie>();

            try
            {
                resultado = _usuarioSerieDbContext.UsuarioSerie.ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public List<UsuarioSerie> MostrarSeriesDeUnUsuario(int id)
        {
            var resultado = new List<UsuarioSerie>();

            try
            {
                resultado = _usuarioSerieDbContext.UsuarioSerie
                                                  .Where(x => x.UsuarioId == id)
                                                  .ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public bool Agregar(UsuarioSerie modelo)
        {
            //List<UsuarioSerie> seriesFavoritasExistentes = MostrarTodo();
            //var seriesFavoritasExistentes = _usuarioSerieDbContext.UsuarioSerie.ToList();

            try
            {
                var serieFavoritasExistentes = _usuarioSerieDbContext.UsuarioSerie.FirstOrDefault(x => x.SerieId == modelo.SerieId  &&
                                                                                              x.UsuarioId == modelo.UsuarioId);

                if (serieFavoritasExistentes != null)
                {
                    return Eliminar(serieFavoritasExistentes.UsuarioSerieId); // Se elimina de favoritas
                }
                else
                {
                    _usuarioSerieDbContext.Add(modelo); // Se agrega a favoritas
                    _usuarioSerieDbContext.SaveChanges();
                }
                
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public UsuarioSerie MostrarUno(UsuarioSerie modelo)
        {
            var resultado = new UsuarioSerie();
            
            try
            {
                resultado = _usuarioSerieDbContext.UsuarioSerie.Single(x => x.UsuarioId == modelo.UsuarioId 
                                                                    && x.SerieId == modelo.SerieId);
            }
            catch (Exception)
            {
            }

            return resultado;
        }
        
        public bool Eliminar(int id)
        {
            try
            {
                UsuarioSerie favorita = (UsuarioSerie)_usuarioSerieDbContext.UsuarioSerie.Where(x => x.UsuarioSerieId == id).First();
                _usuarioSerieDbContext.UsuarioSerie.Remove(favorita);
                _usuarioSerieDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
