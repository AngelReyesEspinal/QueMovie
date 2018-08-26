using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modelo
{
    public class GeneroSerie
    {
        public int GeneroSerieId { get; set; }

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        public int SerieId { get; set; }
        public Serie Serie { get; set; }
    }
}
