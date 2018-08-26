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
    public class UsuariosSeriesController : Controller
    {
        //Dependencia
        private readonly IUsuarioSerieServicio _usuarioSerieServicio;
        private IHostingEnvironment _environment;

        public UsuariosSeriesController(IUsuarioSerieServicio usuarioSerieServicio, IHostingEnvironment environment)
        {
            _usuarioSerieServicio = usuarioSerieServicio;
            _environment = environment;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioSerie modelo)
        {
            return Ok(
                _usuarioSerieServicio.Agregar(modelo)
            );
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                    _usuarioSerieServicio.MostrarTodo()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                    _usuarioSerieServicio.MostrarSeriesDeUnUsuario(id)
            );
        }

        [HttpPost]
        public IActionResult VerificarSerieFavorita([FromBody] UsuarioSerie modelo)
        {
            return Ok(
                    _usuarioSerieServicio.MostrarUno(modelo)
            );
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _usuarioSerieServicio.Eliminar(id)
            );
        }
    }
}