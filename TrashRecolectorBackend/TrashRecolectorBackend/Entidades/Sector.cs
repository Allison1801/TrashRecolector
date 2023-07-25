using System.ComponentModel.DataAnnotations;

namespace TrashRecolectorBackend.Entidades
{
    public class Sector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
