using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;
using TrashRecolectorBackend.Servicios;

namespace TrashRecolectorBackend.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController: ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(AppDbContext dbContext)
        {
            _loginService = new LoginService(dbContext);
        }



        //listar Login de Usuarios
        //[HttpGet]
        //public async Task<IActionResult> ListarLoginAsync(string correo, string contrasena)
        //{


        //    try
        //    {

        //        var login = await _loginService.ObtenerLoginPorCredenciales(correo, contrasena);
        //        if (login != null)
        //        {
        //            return Ok(login);
        //        }
        //        else
        //        {
        //             throw new InvalidOperationException("El usuario no existe");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error al Logonearse:" + ex.Message);
        //    }

        //}

        [HttpGet]
        public async Task<IActionResult> ListarLoginAsync(string correo, string contrasena)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, devuelve un error de validación
                return BadRequest(ModelState);
            }

            try
            {
                var login = await _loginService.ObtenerLoginPorCredenciales(correo, contrasena);
                if (login != null)
                {
                    return Ok(login);
                }
                else
                {
                    throw new InvalidOperationException("El usuario no existe");
                }
            }
           catch (Exception ex)
            {
                return BadRequest("Error al Logonearse: " + ex.Message);
            }
        }
       
    }
}
