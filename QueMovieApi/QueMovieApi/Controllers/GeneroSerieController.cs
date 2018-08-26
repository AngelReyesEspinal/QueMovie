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
    public class GeneroSerieController : Controller
    {
        //Dependencia
        private readonly IGeneroSerieServicio _generoSerieServicio;
        private IHostingEnvironment _environment;

        public GeneroSerieController(IGeneroSerieServicio generoSerieServicio, IHostingEnvironment environment)
        {
            _generoSerieServicio = generoSerieServicio;
            _environment = environment;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]int[] generosId)
        {
            for (int i = 0; i < generosId.Length - 1; i++)
            {
                var modelo = new GeneroSerie();
                modelo.GeneroSerieId = 0;
                modelo.SerieId = generosId[generosId.Length - 1];
                modelo.GeneroId = generosId[i];
                _generoSerieServicio.Agregar(modelo);
            }

            return Ok(
                true
            );
        }
        
        [HttpPost]
        public IActionResult PostEditar([FromBody]int[] generosId)
        {
            for (int i = 0; i < generosId.Length - 1; i++)
            {
                var modelo = new GeneroSerie();
                modelo.GeneroSerieId = 0;
                modelo.SerieId = generosId[generosId.Length - 1];
                modelo.GeneroId = generosId[i];
                _generoSerieServicio.Editar(modelo);
            }

            return Ok(
                true
            );
        }

        [HttpGet("{id}")] //Este es el del dropdown de generos no confundir.
        public IActionResult Get(int id)
        {
            return Ok(
                    _generoSerieServicio.MostrarGenerosDeUnaSerie(id)
            );
        }

        [HttpGet("{id}")] //Este es el de Editar & SerieDetalle
        public IActionResult GetEditar(int id)
        {
            return Ok(
                    _generoSerieServicio.MostrarIdsDeGenerosDeUnaSerie(id)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _generoSerieServicio.Eliminar(id)
            );
        }
    }
}