using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Apoderado
    {
        [Key]
        public int Apoderado_Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Apoderado_Nombre { get; set; }

        [Required(ErrorMessage = "El parentesco es obligatorio")]
        public string Apoderado_Parentesco { get; set; }

        [Required(ErrorMessage = "El número de DNI es obligatorio")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Por favor ingrese un número de DNI válido de 8 dígitos")]
        public string Apoderado_DNI { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [Phone(ErrorMessage = "Por favor ingrese un número de celular válido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de celular debe tener 9 dígitos")]
        public string Apoderado_Celular { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo válido")]
        public string Apoderado_Correo { get; set; }

        public Estudiante Estudiante { get; set; }
    }
}
