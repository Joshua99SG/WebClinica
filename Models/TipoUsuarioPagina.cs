using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clinica.Models
{
    public partial class TipoUsuarioPagina
    {
        [DisplayName("Tipo usuario pagina ID")]
        public int TipoUsuarioPaginaId { get; set; }

        [DisplayName("Tipo usuario ID")]
        public int? TipoUsuarioId { get; set; }

        [DisplayName("Pagina ID")]
        public int? PaginaId { get; set; }

        [DisplayName("Boton Habilitado")]
        public int? BotonHabilitado { get; set; }
    }
}
