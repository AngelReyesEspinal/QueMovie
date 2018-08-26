using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class UsuarioSerie
    {
        public int UsuarioSerieId { get; set; }

        public int SerieId { get; set; }
        public Serie Serie { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
