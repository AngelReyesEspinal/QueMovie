using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Contexto;
using Modelo;
using Microsoft.EntityFrameworkCore;

namespace Servicios
{
    public interface IGeneroServicio
    {
        List<Genero> MostrarTodo();
        Genero MostrarUno(int id);
        bool Agregar(Genero modelo);
        bool Modificar(Genero modelo);
        string MostrarBuscado(int[] generosId);
        bool Eliminar(int id);
        Genero MostrarGeneroBuscado(string genero);
    }

    public class GeneroServicio : IGeneroServicio
    {
        private readonly ApiDbContext _generoDbContext;

        public GeneroServicio(ApiDbContext generoDbContext)
        {
            _generoDbContext = generoDbContext;
        }
        
        public List<Genero> MostrarTodo()
        {
            var resultado = new List<Genero>();

            try
            {
                resultado = _generoDbContext.Genero.ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public bool Agregar(Genero modelo)
        {
            List<Genero> generosExistentes = MostrarTodo();
            
            try
            {
                for (int i = 0; i < generosExistentes.Count; i++)
                {
                    if (generosExistentes[i].Generos == modelo.Generos)
                    {
                        return false;
                    }
                }

                _generoDbContext.Add(modelo);
                _generoDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Genero MostrarUno(int id)
        {
            var resultado = new Genero();

            try
            {
                resultado = _generoDbContext.Genero.Single(x => x.GeneroId == id);
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public string MostrarBuscado(int[] generosId)
        {
            List<Genero> generosExistentes = new List<Genero>();
            string resultado = "";

            try
            {
                for (int i = 0; i < generosId.Length; i++) {
                    generosExistentes.Add(_generoDbContext.Genero.Where(g => g.GeneroId == generosId[i]).Single()); 
                }
                
                foreach (var genero in generosExistentes)
                {
                    resultado += genero.Generos + ", ";
                }
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Genero MostrarGeneroBuscado(string genero)
        {
            var resultado = new Genero();

            try
            {
                resultado = _generoDbContext.Genero.FirstOrDefault(x => x.Generos.Contains(genero));
            }
            catch (Exception ex)
            {
                throw;
            }

            return resultado;
        }

        public bool Modificar(Genero modelo)
        {
            try
            {
                var modeloOriginal = _generoDbContext.Genero.Single(x =>
                    x.GeneroId == modelo.GeneroId);

                modeloOriginal.Generos = modelo.Generos;

                _generoDbContext.Update(modeloOriginal);
                _generoDbContext.SaveChanges();
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
                Genero genero = (Genero)_generoDbContext.Genero.Where(x => x.GeneroId == id).First();
                _generoDbContext.Genero.Remove(genero);
                _generoDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
