using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashRecolectorBackend.Entidades
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener solo letras.")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido debe contener solo letras.")]
        public string Apellido { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La edad debe contener solo números.")]
        //[Range(18,99, ErrorMessage = "La edad debe estar entre 18 y 99.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; }


        [StringLength(8, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 8 caracteres.")]
        public string Contra { get; set; }

        public string estado { get; set; }
       

    }
}
