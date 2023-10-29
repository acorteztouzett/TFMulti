using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Estudiante
    {
        [Key]
        public int Estudiante_Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Estudiante_Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Estudiante_Apellido { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime Estudiante_FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de DNI es obligatorio")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Por favor ingrese un número de DNI válido de 8 dígitos")]
        public string Estudiante_DNI { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [Phone(ErrorMessage = "Por favor ingrese un número de celular válido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de celular debe tener 9 dígitos")]
        public string Estudiante_Celular { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo válido")]
        public string Estudiante_Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{5,}$", ErrorMessage = "La contraseña debe tener al menos 5 caracteres, una letra mayúscula y un número.")]
        public string Estudiante_Contraseña { get; set; }

        public string Estudiante_Foto { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estudiante_Estado { get; set; }


        [ForeignKey("Apoderado")]
        public int? Apoderado_Id { get; set; }
        public Apoderado Apoderado { get; set; }

        public ICollection<EvaluacionPorEstudiante> EvaluacionPorEstudiantes { get; set; }
        public ICollection<EstudiantesPorCurso> EstudiantesPorCursos { get; set; }
        public ICollection<EstudiantePorSesion> EstudiantePorSesions { get; set; }

    }
}
