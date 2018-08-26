using Contexto;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public interface IGeneroSerieServicio
    {
        bool Agregar(GeneroSerie modelo); 
        List<GeneroSerie> MostrarTodo();
        List<int> MostrarGenerosDeUnaSerie(int id);
        List<int> MostrarIdsDeGenerosDeUnaSerie(int id);
        bool Eliminar(int id);
        bool Editar(GeneroSerie modelo);
    }

    public class GeneroSerieServicio : IGeneroSerieServicio
    {
        private readonly ApiDbContext _generoSerieDbContext;

        public GeneroSerieServicio(ApiDbContext generoSerieDbContext)
        {
            _generoSerieDbContext = generoSerieDbContext;
        }

        public List<GeneroSerie> MostrarTodo()
        {
            var resultado = new List<GeneroSerie>();

            try
            {
                resultado = _generoSerieDbContext.GeneroSerie.ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public List<int> MostrarGenerosDeUnaSerie(int id)
        {
            var depurador = new List<GeneroSerie>();
            var seriesDepuradas = new List<int>();
            try
            {
                depurador = _generoSerieDbContext.GeneroSerie
                                                  .Where(x => x.GeneroId == id)
                                                  .ToList();

                for (int i = 0; i < depurador.Count ; i++)
                {
                    seriesDepuradas.Add(depurador[i].SerieId);
                }
            }
            catch (Exception)
            {
            }

            return seriesDepuradas;
        }

        public List<int> MostrarIdsDeGenerosDeUnaSerie(int id)
        {
            var depurador = new List<GeneroSerie>();
            var seriesDepuradas = new List<int>();
            try
            {
                depurador = _generoSerieDbContext.GeneroSerie
                                                  .Where(x => x.SerieId == id)
                                                  .ToList();

                for (int i = 0; i < depurador.Count; i++)
                {
                    seriesDepuradas.Add(depurador[i].GeneroId);
                }
            }
            catch (Exception)
            {
            }

            return seriesDepuradas;
        }

        public bool Eliminar(int id)
        {
            List<GeneroSerie> relacionesExistentes = MostrarTodo();

            try
            {
                for (int i = 0; i < relacionesExistentes.Count ; i++) //Ojala funcione alv
                {
                    GeneroSerie relacionGeneroSerie = (GeneroSerie)_generoSerieDbContext.GeneroSerie.Where(x => x.SerieId == id).First();
                    _generoSerieDbContext.GeneroSerie.Remove(relacionGeneroSerie);
                    _generoSerieDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
        public bool Agregar(GeneroSerie modelo) 
        {
            try
            {
                _generoSerieDbContext.Add(modelo);
                _generoSerieDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Editar(GeneroSerie modelo)
        {
            List<GeneroSerie> relacionesExistentes = MostrarTodo();

            try
            {
                for (int i = 0; i < relacionesExistentes.Count; i++)
                {
                    if (relacionesExistentes[i].SerieId == modelo.SerieId && relacionesExistentes[i].GeneroId == modelo.GeneroId)
                    {
                        return true; // Se elimina de favoritas
                    }
                }

                Agregar(modelo);
            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }
    }
}
