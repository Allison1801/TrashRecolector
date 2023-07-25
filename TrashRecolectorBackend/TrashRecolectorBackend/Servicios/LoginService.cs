using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;

namespace TrashRecolectorBackend.Servicios
{
    public class LoginService
    {
        private readonly AppDbContext _dbContext;

        public LoginService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }


        

        public async Task<Responsive> ObtenerLoginPorCredenciales(string correo, string contrasena)
        {
            var responsive = new Responsive();

          

            try
            {
                var login = await _dbContext.Logins.FirstOrDefaultAsync(u => u.Correo == correo && u.Contra == contrasena);

                if (login != null)
                {
                    // Lógica adicional si se encontró el login
                    responsive.Code = "00";
                    responsive.Data = login;
                    return responsive;
                }
                else
                {
                    // Lógica adicional si no se encontró el login
                    responsive.Code = "01";
                    responsive.Message = "Credenciales inválidas";
                    return responsive;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, registro de errores, etc.
                throw new InvalidOperationException("Error al obtener el usuario por credenciales.", ex);
            }
        }



    }




}
