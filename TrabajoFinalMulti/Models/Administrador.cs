﻿using System.ComponentModel.DataAnnotations;

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
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{5,}$", ErrorMessage = "La contraseña debe tener al menos 5 caracteres, una letra mayúscula y un número.")]
        public string Admin_Contraseña { get; set; }
    }
}
