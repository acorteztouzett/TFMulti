﻿using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Docente
    {
        [Key]
        public int Docente_Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Docente_Nombre { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Por favor ingrese un correo válido")]
        public string Docente_Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Docente_Contraseña { get; set; }
    }
}
