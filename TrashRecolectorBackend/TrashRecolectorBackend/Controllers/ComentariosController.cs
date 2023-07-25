using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashRecolectorBackend.Entidades;
using TrashRecolectorBackend.Servicios;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Azure;

namespace TrashRecolectorBackend.Controllers
{
    [ApiController]
    [Route("api/comentarios")]
    public class ComentariosController: ControllerBase
    {
        Usuario usuario;

        private readonly ComentarioService _comentarioService;

        public ComentariosController(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var responsive = await _comentarioService.ObtenerTodosAsync();
                if (responsive.Code == "00")
                {
                   
                    return Ok(responsive);
                }
                else
                {
                   
                    return BadRequest(responsive);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los comentarios: " + ex.Message);
            }
        }


        
        [HttpPost]
        public async Task<IActionResult> CrearComentario([FromBody] Comentarios inputModel)
        {
            var response = new Responsive();
            if (!ModelState.IsValid)
            {
                response.Code = "01";
                response.Message = "No existe cuerpo de datos";
                // Si el modelo no es válido, devuelve un error de validación
                return Ok(response);
            }

            try
            {
                   var responsive =  await _comentarioService.CrearComentarioAsync(inputModel);

                    if (responsive.Code == "00")
                    {
                        return Ok(responsive);
                    }
                    else
                    {
                        return BadRequest(responsive);
                    }
                
                

            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los comentarios: " + ex.Message);
            }



        }

        }
    }
