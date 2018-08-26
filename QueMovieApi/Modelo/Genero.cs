using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Genero
    {
        public int GeneroId { get; set; }
        public string Generos { get; set; }

        public List<GeneroSerie> Series { get; set; }
    }
}
