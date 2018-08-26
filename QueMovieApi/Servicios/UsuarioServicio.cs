using Contexto;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public interface IUsuarioServicio
    {
        List<Usuario> MostrarTodo();
        Usuario MostrarUno(int id);
        bool Agregar(Usuario modelo);
        bool Modificar(Usuario modelo);
        Usuario ValidarSesion(Usuario modelo);
        Usuario RecuperarContrasenia(Usuario modelo);
        bool Eliminar(int id);
    }

    public class UsuarioServicio : IUsuarioServicio
    {
        //Dependencia
        private readonly ApiDbContext _usuarioDbContext;

        public UsuarioServicio(ApiDbContext usuarioDbContext)
        {
            _usuarioDbContext = usuarioDbContext;
        }

        public bool Agregar(Usuario modelo)
        {
            try
            {
                _usuarioDbContext.Add(modelo);
                _usuarioDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Usuario> MostrarTodo()
        {
            var resultado = new List<Usuario>();

            try
            {
                resultado = _usuarioDbContext.Usuario.ToList();
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Usuario ValidarSesion(Usuario modelo)
        {
            var resultado = new Usuario();
            List<Usuario> usuariosExistentes = MostrarTodo();
            
            try
            {
                for (int i = 0; i < usuariosExistentes.Count; i++)
                {
                    if (usuariosExistentes[i].NombreCuenta == modelo.NombreCuenta && usuariosExistentes[i].Contrasenia == modelo.Contrasenia)
                    {
                        return resultado = usuariosExistentes[i];
                    }
                }
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Usuario RecuperarContrasenia(Usuario modelo)
        {
            var resultado = new Usuario();
            List<Usuario> usuariosExistentes = MostrarTodo();

            try
            {
                for (int i = 0; i < usuariosExistentes.Count; i++)
                {
                    if (usuariosExistentes[i].NombreCuenta == modelo.NombreCuenta && usuariosExistentes[i].PreguntaDeSeguridad == modelo.PreguntaDeSeguridad)
                    {
                        return resultado = usuariosExistentes[i];
                    }
                }
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public Usuario MostrarUno(int id)
        {
            var resultado = new Usuario();

            try
            {
                resultado = _usuarioDbContext.Usuario.Single(x => x.UsuarioId == id);
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public bool Modificar(Usuario modelo)
        {
            try
            {
                var modeloOriginal = _usuarioDbContext.Usuario.Single(x => x.UsuarioId == modelo.UsuarioId);

                modeloOriginal.Nombre        = modelo.Nombre;
                modeloOriginal.Apellido      = modelo.Apellido;
                modeloOriginal.NombreCuenta  = modelo.NombreCuenta;
                modeloOriginal.Contrasenia   = modelo.Contrasenia;
                modeloOriginal.Correo        = modelo.Correo;
                modeloOriginal.Sexo          = modelo.Sexo;
                modeloOriginal.Tipo          = modelo.Tipo;
                modeloOriginal.PreguntaDeSeguridad = modelo.PreguntaDeSeguridad;

                _usuarioDbContext.Update(modeloOriginal);
                _usuarioDbContext.SaveChanges();
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
                Usuario usuario = (Usuario)_usuarioDbContext.Usuario.Where(x => x.UsuarioId == id).First();
                _usuarioDbContext.Usuario.Remove(usuario);
                _usuarioDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
