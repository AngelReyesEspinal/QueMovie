using Contexto;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Servicios
{
    public interface ISerieServicio
    {
        List<Serie> MostrarTodo();
        Serie MostrarUna(int id);
        Serie MostrarBuscada(string titulo);
        IEnumerable<Serie> MostrarTodasLasBuscadas(string titulo);
        IEnumerable<Serie> MostrarPorGenero(int[] seriesId);
        int Agregar(Serie modelo);
        List<Serie> ObteniendoFavoritas(int[] series);
        bool Modificar(Serie modelo);
        bool Eliminar(int id);
    }

    public class SerieServicio : ISerieServicio
    {
        //Dependencia
        private readonly ApiDbContext _serieDbContext;

        public SerieServicio(ApiDbContext serieDbContext)
        {
            _serieDbContext = serieDbContext;
        }

        public int Agregar(Serie modelo)
        {
            var cartaSacrificio = new Serie();
            int id;
            try
            {
                _serieDbContext.Add(modelo);
                _serieDbContext.SaveChanges();

                cartaSacrificio = _serieDbContext.Serie.Where(s => s.Titulo == (modelo.Titulo)).Single();
                id = cartaSacrificio.SerieId; // Sacrificio completado
            }
            catch (Exception)
            {
                return 0;
            }

            return id;
        }

        public List<Serie> MostrarTodo()
        {
            var resultado = new List<Serie>();

            try
            {
                resultado = _serieDbContext.Serie.OrderByDescending(x => x.SerieId).ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Serie MostrarUna(int id)
        {
            var resultado = new Serie();

            try
            {
                resultado = _serieDbContext.Serie.Single(x => x.SerieId == id);
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Serie MostrarBuscada(string titulo)
        {
            var resultado = new Serie();

            try
            {
                resultado = _serieDbContext.Serie.FirstOrDefault(s => s.Titulo.Contains(titulo));
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public IEnumerable<Serie> MostrarPorGenero(int[] seriesId)
        {
            var resultado = new List<Serie>();

            try
            {
                for (int i = 0; i < seriesId.Length ; i++)
                {
                    resultado.Add(_serieDbContext.Serie.Where(s => s.SerieId == seriesId[i]).Single());
                }
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public IEnumerable<Serie> MostrarTodasLasBuscadas(string titulo)
        {
            var resultado = new List<Serie>();

            try
            {
                resultado = _serieDbContext.Serie.Where(s => s.Titulo.Contains(titulo)).ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public List<Serie> ObteniendoFavoritas(int[] series)
        {
            var seriesFavoritas = new List<Serie>();
            List<Serie> seriesExistentes = MostrarTodo();

            try
            {
                for (int i = 0; i < seriesExistentes.Count ; i++)
                {
                    for (int j = 0; j < series.Length; j++)
                    {
                        if (series[j] == seriesExistentes[i].SerieId)
                        {
                            seriesFavoritas.Add(_serieDbContext.Serie.Single(x => x.SerieId == series[j]));
                        }
                    }
                }

                return seriesFavoritas;
            }
            catch (Exception)
            {
            }

            return seriesFavoritas;
        }

        public bool Modificar(Serie modelo)
        {
            try
            {
                var modeloOriginal = _serieDbContext.Serie.Single(x => x.SerieId == modelo.SerieId);
                
                modeloOriginal.Titulo = modelo.Titulo;
                modeloOriginal.Sinopsis = modelo.Sinopsis;
                modeloOriginal.Anio = modelo.Anio;
                modeloOriginal.Productora = modelo.Productora;
                modeloOriginal.Duracion = modelo.Duracion;
                modeloOriginal.Imagen = modelo.Imagen;
                modeloOriginal.Trailer = modelo.Trailer;
                modeloOriginal.Temporadas = modelo.Temporadas;

                _serieDbContext.Update(modeloOriginal);
                _serieDbContext.SaveChanges();
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
                Serie serie = (Serie)_serieDbContext.Serie.Where(x => x.SerieId == id).First();
                _serieDbContext.Serie.Remove(serie);
                _serieDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
