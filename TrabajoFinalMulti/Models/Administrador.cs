using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Administrador
    {
        [Key]
        public int Admin_Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo válido")]
        public string Admin_Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Admin_Contraseña { get; set; }
    }
}
