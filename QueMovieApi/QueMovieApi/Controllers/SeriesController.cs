using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Modelo;
using Servicios;

namespace QueMovieApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SeriesController : Controller
    {
        //Dependencia
        private readonly ISerieServicio _serieDbContext;
        private IHostingEnvironment _environment;

        public SeriesController(ISerieServicio serieServicio, IHostingEnvironment environment)
        {
            _serieDbContext = serieServicio;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                    _serieDbContext.MostrarTodo()
            );
        }

        [HttpPost]
        public IActionResult ObtenerFavoritas([FromBody]int[] series)
        {
            return Ok(
                    _serieDbContext.ObteniendoFavoritas(series)
            );
        }

        [HttpGet("{titulo}")]
        public IActionResult GetBuscadas(string titulo)
        {
            return Ok(
                    _serieDbContext.MostrarTodasLasBuscadas(titulo)
            );
        }

        [HttpPost]
        public IActionResult GetBuscadasPorGenero([FromBody]int[] seriesId)
        {
            return Ok(
                    _serieDbContext.MostrarPorGenero(seriesId)
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _serieDbContext.MostrarUna(id)
            );
        }

        [HttpGet("{titulo}")]
        public IActionResult GetSearched(string titulo)
        {
            return Ok(
                _serieDbContext.MostrarBuscada(titulo)
            );
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm]string Titulo, string Sinopsis,  string Anio, string Productora,
            string Duracion, string Imagen, string Trailer, string Temporadas,IFormFile File)
        {
            var modelo = new Serie(); //Datos del modelo
            string path = @"C:\Users\Angge\Desktop\Proyecto Final Programación 3\my-project\static\Servidor\Series";

            modelo.Titulo = Titulo;
            modelo.Sinopsis = Sinopsis;
            modelo.Anio = Anio;
            modelo.Productora = Productora;
            modelo.Duracion = Duracion;
            modelo.Imagen = Imagen;
            modelo.Trailer = Trailer;
            modelo.Temporadas = Temporadas;
            
            //gestionando el File
            var file = File;
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var filename = Path.Combine(path , file.FileName);

            using (var stream = System.IO.File.OpenWrite(filename))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(_serieDbContext.Agregar(modelo));
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm]int SerieId, string Titulo, string Sinopsis,  string Anio, string Productora,
            string Duracion, string Imagen, string Trailer, string Temporadas, IFormFile File)
        {
            var modelo = new Serie(); //Datos del modelo 
            string path = @"C:\Users\Angge\Desktop\Proyecto Final Programación 3\my-project\static\Servidor\Series";
           
            modelo.SerieId = SerieId;
            modelo.Titulo = Titulo;
            modelo.Sinopsis = Sinopsis;
            modelo.Anio = Anio;
            modelo.Productora = Productora;
            modelo.Duracion = Duracion;
            modelo.Imagen = Imagen;
            modelo.Trailer = Trailer;
            modelo.Temporadas = Temporadas;

            //gestionando el File
            if (File != null)
            { 
                var file = File;
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var filename = Path.Combine(path, file.FileName);

                using (var stream = System.IO.File.OpenWrite(filename))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok(_serieDbContext.Modificar(modelo));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _serieDbContext.Eliminar(id)
            );
        }
    }
}