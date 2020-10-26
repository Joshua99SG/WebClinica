using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class Enfermedad
    {
        [Required(ErrorMessage = "Debe digitar el ID de la Especialidad")]
        [Display(Name = "Enfermedad Id:")]
        public int EnfermedadId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre de la emfermedad")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar la descripcion de la emfermedad")]
        [Display(Name = "Descripcion:")]
        public string Descripcion { get; set; }
    }
}
