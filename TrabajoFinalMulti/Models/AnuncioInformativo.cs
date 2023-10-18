using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class AnuncioInformativo
    {
        [Key]
        public int Anuncio_Id { get; set; }

        [Required(ErrorMessage = "El link es obligatorio")]
        [RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "URL no válida")]
        public string Anuncio_URL { get; set; }

    }
}
