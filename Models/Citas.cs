using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class Citas
    {
        [Display(Name = "Cita ID")]
        public int CitaId { get; set; }
        [Display(Name = "Paciente ID")]
        public int PacienteId { get; set; }
        [Display(Name = "Médico ID")]
        public int MedicoId { get; set; }
        [Display(Name = "Fecha cita")]

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCita { get; set; }
        public string Diagnostico { get; set; }
        [Display(Name = "Especialidad ID")]
        public int? EspecialidadId { get; set; }

        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
