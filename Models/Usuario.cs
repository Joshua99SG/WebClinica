using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "Debe digitar el Id del usuario")]
        [Display(Name ="Usuario ID")]
        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "Debe digitar el nombre del usuario")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe digitar los apellidos del usuario")]
        [Display(Name = "Apellido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Debe digitar la direccion del usuario")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe digitar el telefono del usuario")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        public bool Foto { get; set; }
        [Required(ErrorMessage = "Debe digitar la clave del usuario")]
        [Display(Name = "Clave")]
        public string Clave { get; set; }
    }
}
