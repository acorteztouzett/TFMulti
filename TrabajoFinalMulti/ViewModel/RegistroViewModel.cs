namespace TrabajoFinalMulti.ViewModel
{
    public class RegistroViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public IFormFile Foto { get; set; }
        public string Estado { get; set; }
        public string TipoUsuario { get; set; }
    }
}
