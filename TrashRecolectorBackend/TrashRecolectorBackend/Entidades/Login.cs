using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrashRecolectorBackend.Entidades
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo Contra es obligatorio.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 8 caracteres.")]
        public string Contra { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
