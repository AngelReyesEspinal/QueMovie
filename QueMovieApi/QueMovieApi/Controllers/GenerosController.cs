using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Servicios;

namespace QueMovieApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GenerosController : Controller
    {
        //Dependencia
        private readonly IGeneroServicio _generoServicio;

        public GenerosController(IGeneroServicio generoServicio)
        {
            _generoServicio = generoServicio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_generoServicio.MostrarTodo());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_generoServicio.MostrarUno(id));
        }

        [HttpPost]
        public IActionResult GetSearched([FromBody]int[] generosId)
        {
            return Ok(
                _generoServicio.MostrarBuscado(generosId)
            );
        }

        [HttpGet("{genero}")]
        public IActionResult GetGenero(string genero)
        {
            return Ok(_generoServicio.MostrarGeneroBuscado(genero));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Genero modelo)
        {
            return Ok( _generoServicio.Agregar(modelo));
        }

        [HttpPut]
        public IActionResult Put([FromBody]Genero modelo)
        {
            return Ok(_generoServicio.Modificar(modelo));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_generoServicio.Eliminar(id));
        }
    }
}