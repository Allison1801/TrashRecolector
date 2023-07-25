using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashRecolectorBackend.Entidades
{
    public class Comentarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo Comentario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El comentario debe contener solo letras.")]
        public string Comentario { get; set; }


        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; }  


        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

    }
}
