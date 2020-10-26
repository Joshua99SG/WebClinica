using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class TipoUsuario
    {
        [DisplayName("Tipo Usuario ID")]
        public int TipoUsuarioId { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
