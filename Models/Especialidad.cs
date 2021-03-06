using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Medico = new HashSet<Medico>();
        }

        [Required(ErrorMessage = "Debe digitar el ID de la Especialidad")]
        [Display(Name = "ID:")]
        [DisplayName("ID")]
        public int EspecialidadId { get; set; }

        [Required(ErrorMessage = "Debe digitar el Nombre de la Especialidad")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion:")]
        [Required(ErrorMessage = "Debe digitar la descripción de la Especialidad")]
        public string Descripcion { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
