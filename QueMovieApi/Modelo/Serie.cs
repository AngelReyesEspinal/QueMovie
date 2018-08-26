using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modelo
{
    public class Serie
    {
        public int SerieId { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Anio { get; set; }
        public string Productora { get; set; }  
        public string Duracion { get; set; }
        public string Imagen { get; set; }
        public string Trailer { get; set; }
        public string Temporadas { get; set; }

        public List<UsuarioSerie> Usuarios { get; set; }
        public List<GeneroSerie> Generos { get; set; }
        public List<Capitulo> Capitulos { get; set; }
        
    }
}
