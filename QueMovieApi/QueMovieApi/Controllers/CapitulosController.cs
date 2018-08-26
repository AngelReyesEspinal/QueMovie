using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Servicios;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Modelo.Streaming;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace QueMovieApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CapitulosController : Controller
    {
        //Dependencia
        private readonly ICapituloServicio _capituloServicio;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public CapitulosController(ICapituloServicio capituloServicio)
        {
            _capituloServicio = capituloServicio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_capituloServicio.MostrarTodo());
        }

        [HttpGet("{id}")]
        public IActionResult GetCaps(int id)
        {
            return Ok(_capituloServicio.MostrarTodo(id));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_capituloServicio.MostrarUno(id));
        }

        [HttpPost] // Agregar
        [DisableFormValueModelBinding]
        public async Task<IActionResult> PostAsync()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = @"C:\Users\Angge\Desktop\Proyecto Final Programación 3\QueMovieApi\Capitulos";
            
            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        FileMultipartSection currentFile = section.AsFileSection();
                        string filePath = Path.Combine(targetFilePath, currentFile.FileName);

                        using (var targetStream = System.IO.File.Create(filePath))
                        {
                            await section.Body.CopyToAsync(targetStream).ConfigureAwait(false);
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key"
                        // value

                        // Do not limit the key name length here because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }

                            formAccumulator.Append(key.Value, value);

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }
            
            //Bind form data to a model
            var capitulo = new Capitulo();

            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            var bindingSuccessful = await TryUpdateModelAsync(capitulo, prefix: "",
                valueProvider: formValueProvider);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            var modelo = new Capitulo()
            {
                NumeroCapitulo = capitulo.NumeroCapitulo,
                NombreDelCapitulo = capitulo.NombreDelCapitulo,
                DireccionDelCapitulo = capitulo.DireccionDelCapitulo,
                Temporada = capitulo.Temporada,
                SerieId = capitulo.SerieId
            };

            return Ok(_capituloServicio.Agregar(modelo));
        }

        [HttpPut] // Editar
        [DisableFormValueModelBinding]
        public async Task<IActionResult> PutAsync()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = @"C:\Users\Angge\Desktop\Proyecto Final Programación 3\QueMovieApi\Capitulos";

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        FileMultipartSection currentFile = section.AsFileSection();
                        string filePath = Path.Combine(targetFilePath, currentFile.FileName);

                        using (var targetStream = System.IO.File.Create(filePath))
                        {
                            await section.Body.CopyToAsync(targetStream).ConfigureAwait(false);
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key"
                        // value

                        // Do not limit the key name length here because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }

                            formAccumulator.Append(key.Value, value);

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            //Bind form data to a model
            var capitulo = new Capitulo();

            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            var bindingSuccessful = await TryUpdateModelAsync(capitulo, prefix: "",
                valueProvider: formValueProvider);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            var modelo = new Capitulo()
            {
                CapituloID = capitulo.CapituloID,
                NumeroCapitulo = capitulo.NumeroCapitulo,
                NombreDelCapitulo = capitulo.NombreDelCapitulo,
                DireccionDelCapitulo = capitulo.DireccionDelCapitulo,
                Temporada = capitulo.Temporada,
                SerieId = capitulo.SerieId
            };

            return Ok(_capituloServicio.Modificar(modelo));
        }

        
        [HttpGet("{nombreDelVideo}")]//Mostrar Video
        public IActionResult Video(string nombreDelVideo)
        {
            string pathToVideos = @"C:\Users\Angge\Desktop\Proyecto Final Programación 3\QueMovieApi\Capitulos";
            var path = Path.Combine(pathToVideos, nombreDelVideo);
            return File(System.IO.File.OpenRead(path), "video/mkv");
        }

        //Esto no sé lo que hace :v
        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_capituloServicio.Eliminar(id));
        }
    }
}