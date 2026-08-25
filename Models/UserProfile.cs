using System;
using System.Collections.Generic;
using System.Text;

namespace PerfilSolMAUI.Models
{
    // El modelo almacena únicamente la estructura de datos del perfil
    public class UserProfile
    {
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
    }
}