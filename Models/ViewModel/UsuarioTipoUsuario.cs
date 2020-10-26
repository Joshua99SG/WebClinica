using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models.ViewModel
{
    public class UsuarioTipoUsuario
    {
        [DisplayName("Usuario ID")]
        public int UsuarioId { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Tipo usuario Id")]
        public int TipoUsuarioId { get; set; }
        [DisplayName("Tipo Usuario Nombre")]
        public string TipoUsuarioNombre { get; set; }
    }
}
