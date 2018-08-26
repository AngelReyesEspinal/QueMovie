using Contexto;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public interface IUsuarioCapituloServicio
    {
        bool Agregar(UsuarioCapitulo modelo);
        bool Eliminar(int id);
        bool Modificar(UsuarioCapitulo modelo);
        List<UsuarioCapitulo> MostrarComentariosDeUnCapitulo(int id);
    }

    public class UsuarioCapituloServicio : IUsuarioCapituloServicio
    {
        //Dependencia
        private readonly ApiDbContext _usuarioCapituloDbContext;

        public UsuarioCapituloServicio(ApiDbContext usuarioCapituloDbContext)
        {
            _usuarioCapituloDbContext = usuarioCapituloDbContext;
        }

        public bool Agregar(UsuarioCapitulo modelo)
        {
            try
            {
                _usuarioCapituloDbContext.Add(modelo);
                _usuarioCapituloDbContext.SaveChanges();
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
                UsuarioCapitulo comentario = (UsuarioCapitulo) _usuarioCapituloDbContext.UsuarioCapitulo.Where(x => x.UsuarioCapituloId == id).First();
                _usuarioCapituloDbContext.UsuarioCapitulo.Remove(comentario);
                _usuarioCapituloDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Modificar(UsuarioCapitulo modelo)
        {
            try
            {
                var modeloOriginal = _usuarioCapituloDbContext.UsuarioCapitulo.Single(x => x.UsuarioCapituloId == modelo.UsuarioCapituloId);

                modeloOriginal.CapituloID = modelo.CapituloID;
                modeloOriginal.UsuarioId = modelo.UsuarioId;
                modeloOriginal.Comentario = modelo.Comentario;
                modeloOriginal.Comentador = modelo.Comentador;
                modeloOriginal.Editado = modelo.Editado;

                _usuarioCapituloDbContext.Update(modeloOriginal);
                _usuarioCapituloDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        public List<UsuarioCapitulo> MostrarComentariosDeUnCapitulo(int id)
        {
            var resultado = new List<UsuarioCapitulo>();

            try
            {
                resultado = _usuarioCapituloDbContext.UsuarioCapitulo
                                                     .Where(x => x.CapituloID == id)
                                                     .OrderByDescending(x => x.UsuarioCapituloId)
                                                     .ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }
    }
}
