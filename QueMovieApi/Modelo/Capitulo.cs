using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modelo
{
    public class Capitulo
    {
        public int CapituloID { get; set; }
        public int NumeroCapitulo { get; set; }
        public string NombreDelCapitulo { get; set; }
        public string DireccionDelCapitulo { get; set; }
        public string Temporada { get; set; }
        
        public int SerieId { get; set; }
        public Serie Serie { get; set; }

        public List<UsuarioCapitulo> Usuarios { get; set; }
    }
}
