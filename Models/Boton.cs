using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clinica.Models
{
    public partial class Boton
    {
        [DisplayName("Boton Id")]
        public int BotonId { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }

        [DisplayName("Boton habilitado")]
        public int? BotonHabilitado { get; set; }
    }
}
