using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashRecolectorBackend.Data;
using TrashRecolectorBackend.Entidades;


namespace TrashRecolectorBackend.Servicios
{
    public class UsuarioService
    {
        private readonly AppDbContext _dbContext;

        ComentarioService  comentarioService;
        LoginService loginService;

        public UsuarioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            this.comentarioService = new ComentarioService(dbContext);
        }

        //Listar Usuarios
        public async Task<Responsive> ListarUsuariosAsync()
        {
            var responsive = new Responsive();

            try
            {
                var usuarios = await _dbContext.Usuarios.ToListAsync();

                responsive.Code = "00";
                responsive.Message = "Operación exitosa";
                responsive.Data = usuarios;
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al listar los usuarios";
               
            }

            return responsive;
        }

       

        public async Task<Responsive> ObtenerUsuarioPorCredenciales(string correo, string contrasena)
        {
            var responsive = new Responsive();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                responsive.Code = "01";
                responsive.Message = "Ingrese las credenciales";
                return responsive;
            }

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Contra == contrasena);

                if (usuario != null)
                {
                    responsive.Code = "00";
                    responsive.Message = "Operación exitosa";
                    responsive.Data = usuario;
                }
                else
                {
                    responsive.Code = "01";
                    responsive.Message = "Las credenciales no existen";
                }
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al obtener el usuario por credenciales";
                
            }

            return responsive;
        }


       

        public async Task<Responsive> ObtenerUsuarioPorId(int id)
        {
            var responsive = new Responsive();

            if (id == 0)
            {
                responsive.Code = "01";
                responsive.Message = "El campo 'id' es obligatorio";
                return responsive;
            }

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.Id == id);

                if (usuario != null)
                {
                    responsive.Code = "00";
                    responsive.Message = "Operación exitosa";
                    responsive.Data = usuario;
                }
                else
                {
                    responsive.Code = "01";
                    responsive.Message = "El id no existe";
                }
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al obtener el usuario por id";
              
        
            }

            return responsive;
        }



        public async Task<Responsive> ObtenerUsuarioPorCorreo(string correo)
            {
                var responsive = new Responsive();

                if (string.IsNullOrEmpty(correo))
                {
                    responsive.Code = "01";
                    responsive.Message = "Ingrese el correo";
                    return responsive;
                }

                try
                {
                    var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.Correo == correo);

                    if (usuario != null)
                    {
                        responsive.Code = "00";
                        responsive.Message = "Operación exitosa";
                        responsive.Data = usuario;
                    }
                    else
                    {
                        responsive.Code = "01";
                        responsive.Message = "El correo no existe";
                    }
                }
                catch (Exception ex)
                {
                    responsive.Code = "01";
                    responsive.Message = "Error al obtener el usuario por correo";
                   
            
                }

                return responsive;
            }



        public async Task<Responsive> ObtenerUsuarioPorCorreoContrasena(string correo)
        {
            var responsive = new Responsive();

            if (string.IsNullOrEmpty(correo))
            {
                responsive.Code = "01";
                responsive.Message = "Ingrese el correo";
                return responsive;
            }

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.Correo == correo);

                if (usuario != null)
                {
                    responsive.Code = "00";
                    responsive.Message = "Te hemos enviado la contraseña a tu correo";
                    responsive.Data = usuario;
                   
                }
                else
                {
                    responsive.Code = "01";
                    responsive.Message = "El correo no existe";
                }
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al obtener el usuario por correo";


            }
            Console.WriteLine($"Respuesta generada: {JsonConvert.SerializeObject(responsive)}");
            return responsive;
        }




        public async Task<Responsive> CrearUsuarioAsync(string nombre, string apellido, int edad, string correo, string estado)
        {
            var responsive = new Responsive();

            // Generar contraseña aleatoria
            string contra = GenerateRandomPassword(8); // Cambia la longitud de la contraseña según tus necesidades

            var usua = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (usua == null)
            {
                var usuario = new Usuario
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Edad = edad,
                    Correo = correo,
                    Contra = contra,
                    estado = estado
                };

                _dbContext.Usuarios.Add(usuario);
                await _dbContext.SaveChangesAsync();

                var login = new Login
                {
                    Correo = correo,
                    Contra = contra,
                    UsuarioId = usuario.Id
                };

                _dbContext.Logins.Add(login);
                await _dbContext.SaveChangesAsync();

                responsive.Code = "00";
                responsive.Message = "Usuario creado exitosamente";
                responsive.Data = usuario;
            }
            else
            {
                responsive.Code = "01";
                responsive.Message = "Usuario ya registrado";
            }

            return responsive;
        }

      

        public string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<Responsive> CambiarContrasena(int id, string nuevaContrasena)
        {
            var responsive = new Responsive();

            if (string.IsNullOrEmpty(nuevaContrasena))
            {
                responsive.Code = "01";
                responsive.Message = "La contraseña no puede estar vacía";
                return responsive;
            }

            if (nuevaContrasena.Length < 6 || nuevaContrasena.Length > 8)
            {
                responsive.Code = "01";
                responsive.Message = "La contraseña debe tener entre 6 y 8 caracteres";
                return responsive;
            }

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario != null)
                {
                    // Aquí actualizamos solo el campo de la contraseña en el objeto existente.
                    usuario.Contra = nuevaContrasena;
                    await _dbContext.SaveChangesAsync();

                    responsive.Code = "00";
                    responsive.Message = "Contraseña actualizada correctamente";
                    responsive.Data = usuario;
                }
                else
                {
                    responsive.Code = "01";
                    responsive.Message = "El usuario no existe";
                }
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al cambiar la contraseña: " + ex.Message;
            }

            return responsive;
        }

        public async Task<Responsive> ModificarUsuarioAsync(int usuarioId, string nombre, string apellido, int edad, string correo, string contra)
        {
            var responsive = new Responsive();
          
            var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

            if (usuario == null)
            {
                responsive.Code = "01";
                responsive.Message = "El usuario no existe";
                return responsive;
            }

            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Edad = edad;
            usuario.Correo = correo;
            usuario.Contra = contra;

            await _dbContext.SaveChangesAsync();

            var login = await _dbContext.Logins.FindAsync(usuarioId);

            if (login != null)
            {
                login.Correo = correo;
                login.Contra = contra;

                await _dbContext.SaveChangesAsync();
            }

            responsive.Code = "00";
            responsive.Message = "Usuario modificado exitosamente";
            responsive.Data = usuario;

            return responsive;
        }

        public async Task<Responsive> EliminarUsuarioAsync(int usuarioId)
        {
            var responsive = new Responsive();

            if (usuarioId == 0)
            {
                responsive.Code = "01";
                responsive.Message = "El campo 'usuarioId' es obligatorio";
                return responsive;
            }

            var usuario = await _dbContext.Usuarios.FindAsync(usuarioId);

            if (usuario == null)
            {
                responsive.Code = "01";
                responsive.Message = "El usuario no existe";
                return responsive;
            }

            try
            {
                _dbContext.Usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();

                var login = await _dbContext.Logins.FindAsync(usuarioId);

                if (login != null)
                {
                    _dbContext.Logins.Remove(login);
                    await _dbContext.SaveChangesAsync();
                }

                responsive.Code = "00";
                responsive.Message = "Usuario eliminado exitosamente";
            }
            catch (Exception ex)
            {
                responsive.Code = "01";
                responsive.Message = "Error al eliminar el usuario";
               
            }

            return responsive;
        }




    }
}
