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
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe digitar el tipo de usuario")]
        [Display(Name = "Tipo Usuario")]
        public int TipoUsuarioId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre del usuario")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar la clave del usuario")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
