using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clinica.Models
{
    public partial class TipoUsuarioPaginaBoton
    {
        [DisplayName("Tipo usuario pagina boton ID")]
        public int TipoUsuarioPaginaBotonId { get; set; }

        [DisplayName("Tipo usuario pagina ID")]
        public int? TipoUsuarioPaginaId { get; set; }

        [DisplayName("Boton ID")]
        public int? BotonId { get; set; }

        [DisplayName("Boton Habilitado")]
        public int? BotonHabilitado { get; set; }
    }
}
