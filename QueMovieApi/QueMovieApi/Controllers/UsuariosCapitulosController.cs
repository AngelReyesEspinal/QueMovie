using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Servicios;

namespace QueMovieApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuariosCapitulosController : Controller
    {
        //Dependencia
        private readonly IUsuarioCapituloServicio _usuarioCapituloServicio;
        private IHostingEnvironment _environment;

        public UsuariosCapitulosController(IUsuarioCapituloServicio usuarioCapituloServicio, IHostingEnvironment environment)
        {
            _usuarioCapituloServicio = usuarioCapituloServicio;
            _environment = environment;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioCapitulo modelo)
        {
            return Ok(
                _usuarioCapituloServicio.Agregar(modelo)
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                    _usuarioCapituloServicio.MostrarComentariosDeUnCapitulo(id)
            );
        }

        [HttpPut]
        public IActionResult Put([FromBody]UsuarioCapitulo modelo)
        {
            return Ok(
                _usuarioCapituloServicio.Modificar(modelo)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _usuarioCapituloServicio.Eliminar(id)
            );
        }
    }
}