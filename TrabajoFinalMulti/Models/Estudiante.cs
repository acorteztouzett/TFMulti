using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Estudiante
    {
        [Key]
        public int Estudiante_Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Estudiante_Nombre { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo válido")]
        public string Estudiante_Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{5,}$", ErrorMessage = "La contraseña debe tener al menos 5 caracteres, una letra mayúscula y un número.")]
        public string Estudiante_Contraseña { get; set; }
    }
}
