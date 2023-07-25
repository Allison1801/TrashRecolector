using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;

namespace TrashRecolectorBackend.Servicios
{
    public class SectorService
    {
        private readonly AppDbContext _dbContext;
        public SectorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Listar Sectores
        //public async Task<List<Sector>> ListarSectoresAsync()
        //{
        //    return await _dbContext.Sectores.ToListAsync();
        //}

        public async Task<ResponsiveList<List<Sector>>> ListarSectoresAsync()
        {
            var responsive = new ResponsiveList<List<Sector>>();

            try
            {
                var sectores = await _dbContext.Sectores.ToListAsync();

                responsive.Code = "00";
                responsive.Message = "Operación exitosa";
                responsive.Data = sectores;
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al listar los sectores";
               
                
            }

            return responsive;
        }


    }


}
