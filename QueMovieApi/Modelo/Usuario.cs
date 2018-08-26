using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCuenta { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string Tipo { get; set; }
        public string PreguntaDeSeguridad { get; set; }

        public List<UsuarioSerie> Series { get; set; }
        public List<UsuarioCapitulo> Capitulos { get; set; }
    }
}
