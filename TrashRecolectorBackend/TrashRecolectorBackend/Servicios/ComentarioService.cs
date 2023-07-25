using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;
using Microsoft.EntityFrameworkCore;
using Azure;


namespace TrashRecolectorBackend.Servicios
{
   
    public class ComentarioService
    {
        private readonly AppDbContext _dbContext;
        Usuario usuario;
       

        public ComentarioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            this.usuario = new Usuario();
           
        }

        public async  Task<Responsive> ObtenerTodosAsync()
        {
            var responsive = new Responsive();
            try
            {
              var ListComentarios=  await _dbContext.Comentarios.ToListAsync();

                responsive.Code = "00";
                responsive.Message = "Operación exitosa";
                responsive.Data = ListComentarios;
            }
            catch
            {
                responsive.Code = "01";
                responsive.Message = "Error al obtener la lista de comentarios";
            }
            return responsive;
            
        }


        public async Task<Responsive> CrearComentarioAsync(Comentarios comentario)
        {
            var responsive = new Responsive();

            if (comentario == null)
            {
                responsive.Code = "01";
                responsive.Message = "El comentario es nulo";
                return responsive;
            }

            if (string.IsNullOrEmpty(comentario.Correo) || (string.IsNullOrEmpty(comentario.Comentario)))
            {
                responsive.Code = "01";
                responsive.Message = "Ingrese el correo y el comentario";
                return responsive;
            }

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == comentario.Correo);

                if (usuario == null)
                {
                    responsive.Code = "01";
                    responsive.Message = "El usuario no existe";
                }
                else
                {
                    comentario.Usuario = usuario;
                    _dbContext.Comentarios.Add(comentario);
                    await _dbContext.SaveChangesAsync();

                    responsive.Code = "00";
                    responsive.Message = "Comentario creado exitosamente";
                    responsive.Data = comentario;
                }
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al crear el comentario";
        
            }

            return responsive;
        }


    }
}
