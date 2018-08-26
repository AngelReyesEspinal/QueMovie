using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class UsuarioCapitulo
    {
        public int UsuarioCapituloId { get; set; }
        public string Comentario { get; set; }
        public string Comentador { get; set; }
        public string Editado { get; set; }
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int CapituloID { get; set; }
        public Capitulo Capitulo { get; set; }
    }
}
