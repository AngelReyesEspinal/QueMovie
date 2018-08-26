using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Servicios;

namespace QueMovieApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuariosController : Controller
    {
        //Dependencia
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuariosController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                    _usuarioServicio.MostrarTodo()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _usuarioServicio.MostrarUno(id)
            );
        }

        [HttpPost]
        public IActionResult Post([FromBody]Usuario modelo)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("quemovieinc@gmail.com");
                msg.To.Add(modelo.Correo);
                msg.Subject = "Correo de bienvenida.";
                msg.Body =
                    "<div>" +
                        "<h1>¡Bienvenido a nuestra comunidad!</h1>" +
                        "<br>" +
                        "<p> Nos complace mucho el hecho de que ahora formes parte de esta gran familia, " +
                        "nuestro objetivo es que cada vez que entres a QueMovie!, puedas olvidar por un momento " +
                        "los problemas y solo disfrutes de los contenidos que tenemos preparados para tí " +
                         modelo.Nombre + " " + modelo.Apellido + "." +
                         "<br>" +
                        "<p>Atentamente QueMovie!</p>" +
                    "</div>";
                msg.IsBodyHtml = true;
                MailAddress ms = new MailAddress(modelo.Correo);

                msg.CC.Add(ms);
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                sc.Port = 587;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("quemovieinc@gmail.com", "santaclos4");
                sc.EnableSsl = true;
                sc.Send(msg);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(
                _usuarioServicio.Agregar(modelo)
            );
        }

        [HttpPost]
        public IActionResult IniciarSesion([FromBody]Usuario modelo)
        {
            return Ok(
                    _usuarioServicio.ValidarSesion(modelo)
            );
        }

        [HttpPost]
        public IActionResult RecuperarContrasenia ([FromBody]Usuario modelo)
        {
            return Ok(
                    _usuarioServicio.RecuperarContrasenia(modelo)
            );
        }

        [HttpPut]
        public IActionResult Put([FromBody]Usuario modelo)
        {
            return Ok(
                _usuarioServicio.Modificar(modelo)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _usuarioServicio.Eliminar(id)
            );
        }
    }
}
