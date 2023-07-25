using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;
using TrashRecolectorBackend.Servicios;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace TrashRecolectorBackend.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(AppDbContext dbContext)
        {
            _usuarioService = new UsuarioService(dbContext);
        }


    
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarioPorCredenciales(string correo, string contrasena)
        {
            
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorCredenciales(correo, contrasena);

                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("correo")]
        public async Task<IActionResult> ObtenerUsuarioPorCorreo(string correo)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorCorreo(correo);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("correo/{correo}")]
        public async Task<IActionResult> ObtenerUsuarioPorCorreoContra(string correo)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorCorreoContrasena(correo);
                if (usuario != null)
                {
                    // Enviar correo electrónico
                    string subject = "Recuperación de contraseña TrashRecolector";
                    string body = $"Estimado {(usuario.Data as Usuario)?.Nombre}, Su contraseña actual es la siguiente:<br>Contraseña: {(usuario.Data as Usuario)?.Contra}";

                    await SendEmail(correo, subject, body);
                   
                    return Ok(usuario);

                }
                else
                {
                    return NotFound();
                }

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/cambiar-contrasena")]
        public async Task<IActionResult> CambiarContrasena(int id, [FromBody] Usuario usuarioActualizado)
        {
            var result = await _usuarioService.CambiarContrasena(id, usuarioActualizado.Contra);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario inputModel)
        {
            var response = new Responsive();

            if (!ModelState.IsValid)
            {
                response.Code = "01";
                response.Message = "Digite datos correctos";
                // Si el modelo no es válido, devuelve un error de validación
                return Ok(response);
            }

            try
            {
                var responsive = await _usuarioService.CrearUsuarioAsync(inputModel.Nombre, inputModel.Apellido, inputModel.Edad, inputModel.Correo, inputModel.estado);

                if (responsive.Code == "00")
                {
                    // Enviar correo electrónico
                    string subject = "Registro de Usuario TrashRecolector";
                    string body = $"Estimado Usuario, se ha registrado con éxito en la página TrashRecolector. Sus credenciales son las siguientes:<br>Correo electrónico: {inputModel.Correo}<br>Contraseña: {(responsive.Data as Usuario)?.Contra}";

                    await SendEmail(inputModel.Correo, subject, body);

                    // Usuario creado exitosamente
                    return Ok(responsive);
                }
                else
                {
                    // Usuario ya registrado o campos obligatorios faltantes
                    return Ok(responsive);
                }
            }
            catch (InvalidOperationException ex)
            {
                return Ok(ex.Message);
            }
        }

        async Task SendEmail(string recipient, string subject, string body)
        {
            var apiKey = "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"; // Reemplaza con tu clave de API de Sendinblue
            var senderEmail = "allison-holguin@hotmail.com"; // Reemplaza con tu dirección de correo electrónico
            var senderName = "TrashRecolector";
            var recipientEmail = recipient;

            var url = "https://api.sendinblue.com/v3/smtp/email";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("api-key", apiKey);

                var payload = new
                {
                    sender = new { name = senderName, email = senderEmail },
                    to = new[] { new { email = recipientEmail } },
                    cc = new[] { new { email = "aholguinba@ug.edu.ec" } }, 
                    subject = subject,
                    htmlContent = body
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Correo electrónico enviado correctamente");
                }
                else
                {
                    Console.WriteLine("Error al enviar el correo electrónico: " + response.Content.ReadAsStringAsync().Result);
                }
            }
        }


        //Modificar Usuarios
        [HttpPut("{usuarioId:int}")]
        public async Task<IActionResult> ModificarUsuario(int usuarioId, [FromBody] Usuario inputModel)
        {
          
            try
            {
                var  responsive = await _usuarioService.ModificarUsuarioAsync(usuarioId, inputModel.Nombre, inputModel.Apellido, inputModel.Edad, inputModel.Correo, inputModel.Contra);
                if(responsive.Code == "00")
                {
                    return Ok(responsive);
                }
                else
                {
                    return BadRequest(responsive);
                }
              
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // Eliminar Usuario
        [HttpDelete("{usuarioid}")]
        public async Task<IActionResult> EliminarUsuario(int usuarioid)
        {
            try
            {
                var responsive = await _usuarioService.EliminarUsuarioAsync(usuarioid);

                if (responsive.Code == "00")
                {
                    // Usuario eliminado exitosamente
                    return Ok(responsive);
                }
                else
                {
                    // El usuario no existe o error al eliminarlo
                    return BadRequest(responsive);
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }

}

