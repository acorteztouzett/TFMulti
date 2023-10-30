using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Docente
    {
        [Key]
        public int Docente_Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Docente_Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Docente_Apellido { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime Docente_FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de DNI es obligatorio")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Por favor ingrese un número de DNI válido de 8 dígitos")]
        public string Docente_DNI { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [Phone(ErrorMessage = "Por favor ingrese un número de celular válido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de celular debe tener 9 dígitos")]
        public string Docente_Celular { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Por favor ingrese un correo válido")]
        public string Docente_Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{5,}$", ErrorMessage = "La contraseña debe tener al menos 5 caracteres, una letra mayúscula y un número.")]
        public string Docente_Contraseña { get; set; }

        public string Docente_Foto { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Docente_Estado { get; set; }

        public List<Curso> Curso { get; set; }
    }
}
