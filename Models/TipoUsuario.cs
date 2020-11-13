using Clinica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [DisplayName("Tipo Usuario ID")]
        public int TipoUsuarioId { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion:")]
        [StringLength(400, ErrorMessage = "Ha excedido los 400 caracteres")]
        [Required(ErrorMessage = "Debe digitar la descripción de la Especialidad")]
        public string Descripcion { get; set; }

        public int? BotonHabilitado { get; set; }

        public virtual ICollection<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }

    }
}
