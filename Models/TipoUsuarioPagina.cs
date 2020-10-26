using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clinica.Models
{
    public partial class TipoUsuarioPagina
    {
        [DisplayName("Tipo usuario pagina ID")]
        public int TipousuariopaginaId{ get; set; }

        [DisplayName("Tipo usuario ID")]
        public int? TipousuarioId { get; set; }

        [DisplayName("Pagina ID")]
        public int? Paginaid { get; set; }

        [DisplayName("Boton Habilitado")]
        public int? BotonHabilitado { get; set; }
    }
}
