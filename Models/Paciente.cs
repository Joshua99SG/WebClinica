using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Citas = new HashSet<Citas>();
        }

        [Required(ErrorMessage = "Debe digitar el ID del paciente")]
        [Display(Name = "ID:")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre del paciente")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar el apellido del paciente")]
        [Display(Name = "Apellido:")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe digitar la direccion del paciente")]
        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe digitar el telefono")]
        [Display(Name = "Telefono:")]
        public string TelefonoContacto { get; set; }

        [Required(ErrorMessage = "Por favor incluya una dirección de correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",
            ErrorMessage = "Ingrese una dirección de correo válida")]
        public string Email { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
