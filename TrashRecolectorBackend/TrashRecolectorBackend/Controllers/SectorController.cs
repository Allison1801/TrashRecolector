using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Servicios;

namespace TrashRecolectorBackend.Controllers
{
    [ApiController]
    [Route("api/sector")]
    public class SectorController: ControllerBase
    {
        private readonly SectorService _sectorService;

        public SectorController(AppDbContext dbContext)
        {
            _sectorService = new SectorService(dbContext);
            
        }

        //listar Sectores
        [HttpGet]
        public async Task<IActionResult> ListarSectores()
        {
            try
            {
                var responsive = await _sectorService.ListarSectoresAsync();
                if (responsive.Code == "00")
                {
                    // Operación exitosa
                    return Ok(responsive);
                }
                else
                {
                    // Error al obtener la lista de usuarios
                    return Ok(responsive);
                }
            }

            catch (Exception ex)
            {
                return Ok("Error al listar los sectores: " + ex.Message);
            }
            
        }

        
       


    }
}
